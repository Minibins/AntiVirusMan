using DustyStudios;
using DustyStudios.TextAVM;

using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EasterEggsForDummies : Upgrade
{
    public static bool isLookingForReferences =true;
    [SerializeField] GameObject glass;
    public static Text hintText;
    public static Transform Glass;
    private void Awake()
    {
        Glass = glass.transform;
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
    }
}
