using DustyStudios;
using DustyStudios.TextAVM;

using System.Collections;
using System.Drawing.Imaging;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EasterEggsForDummies : Upgrade
{
    public static bool isLookingForReferences =true;
    [SerializeField] GameObject glass;
    private static SpriteRenderer glassRenderer;
    public static Text hintText;
    public static Transform Glass;
    static Color glassColor
    {
        set
        {
            glassRenderer.color = value;
            hintText.color = value;
        }
    }
    private void Awake()
    {
        Glass = glass.transform;
        glassRenderer = Glass.GetComponentInChildren<SpriteRenderer>();
        hintText = Glass.GetComponentInChildren<Text>();
    }
    [DustyConsoleCommand("lookref","Use Easter Eggs For Dummies")]
    public static string LookRef()
    {
        isLookingForReferences=true;
        return "Now you should click anywhere";
    }
    void Update()
    {
        if(!isLookingForReferences)
            return;
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(MoveGlassToPos(Camera.main.ScreenToWorldPoint(Input.mousePosition),0.1f,true));
            StartCoroutine(StertchStringTo0());
        }
        else if(Input.touchCount > 0 || Input.touches.Any(t => t.phase == TouchPhase.Began))
        {
            StartCoroutine(MoveGlassToPos(Camera.main.ScreenToWorldPoint(Input.touches.Where(t=>t.phase == TouchPhase.Began).First().position), 0.1f,true));
            StartCoroutine(StertchStringTo0());
        }
        IEnumerator StertchStringTo0()
        {
            float a = 0.1f;
            string text = hintText.text;
            for(float i = 0; 1+i+a*i-(i*i) > 0; i+=Time.deltaTime*2)
            {
                hintText.text = TextA.Stretch(text,1 + i + a * i - i * i);
                yield return new WaitForEndOfFrame();
            }
            hintText.text = "";
        }
    }
    private static bool isGlassMoves;
    public static IEnumerator MoveGlassToPos(Vector3 pos,float time,bool isChecksNextFrame)
    {
        glassColor = Color.white;
        EasterEggsForDummies.isLookingForReferences = false;
        Vector3 vector = Vector3.zero;
        pos.z = 0;
        if(isChecksNextFrame)
        {
            yield return new WaitForEndOfFrame();
            if(isGlassMoves) yield break;
        }
        isGlassMoves = true;
        for(float i = time; i > 0; i -= Time.deltaTime)
        {
            Glass.position = Vector3.SmoothDamp(Glass.position,pos,ref vector,i,999,Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        isGlassMoves=false;
        yield return new WaitForSeconds(1.7f);
        while(!isGlassMoves&&glassRenderer.color.a!=0)
        {
            glassColor = new(glassRenderer.color.r,glassRenderer.color.g,glassRenderer.color.b,glassRenderer.color.a-Time.deltaTime*0.8f);
            yield return new WaitForEndOfFrame();
        }
    }
}
