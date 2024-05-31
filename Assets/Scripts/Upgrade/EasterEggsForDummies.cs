using DustyStudios;
using DustyStudios.MathAVM;
using DustyStudios.TextAVM;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using static UnityEngine.Rendering.DebugUI;

public class EasterEggsForDummies : Upgrade
{
    public static bool isLookingForReferences = false;
    [SerializeField] GameObject glass, glassUI;
    private static SpriteRenderer glassRenderer;
    private static Image glassRendererUI;
    public static Text hintText, hintText2;
    public static Transform Glass;
    public static RectTransform GlassUI;
    private static PointEffector2D GlassCollider;
    static Color glassColor
    {
        set
        {
            glassRendererUI.color = value;
            glassRenderer.color = value;
            hintText.color = value;
            hintText2.color = value;
            GlassCollider.forceMagnitude = value.a * 0.7f;
        }
    }
    private void Awake()
    {
        _weight = (uint)PlayerPrefs.GetInt("beatenEasterEvent");
        Glass = glass.transform;
        GlassUI = glassUI.GetComponent<RectTransform>();
        glassRenderer = Glass.GetComponentInChildren<SpriteRenderer>();
        glassRendererUI = glassUI.GetComponent<Image>();
        hintText = Glass.GetComponentInChildren<Text>();
        hintText2 = GlassUI.GetComponentInChildren<Text>();
        GlassCollider = Glass.GetComponentInChildren<PointEffector2D>();
        SceneManager.sceneUnloaded += (s) => ClearFoundRefs();
    }
    private void ClearFoundRefs()
    {
        ReferenceItem.foundReferenceNames.Clear();
        SceneManager.sceneUnloaded -= (s) => ClearFoundRefs();
    }
    public static void SetGlassPos(Vector2 value,bool UI)
    {
        if(UI)
            GlassUI.position = value;
        else Glass.position = Camera.main.ScreenToWorldPoint(value).Multiply(new(1,1,0));
    }
    public static void SetGlassPos(Vector3 value,bool UI)
    {
        if(UI) GlassUI.position = value;
        else Glass.position = value;
    }
    public void LookRefr() => isLookingForReferences = true;
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
            StartCoroutine(MoveGlassToPos(Input.mousePosition,0.1f,true, false));
            StartCoroutine(StertchStringTo0());
        }
        else if(Input.touchCount > 0 || Input.touches.Any(t => t.phase == TouchPhase.Began))
        {
            StartCoroutine(MoveGlassToPos(Input.touches.Where(t=>t.phase == TouchPhase.Began).First().position, 0.1f,true,false));
            StartCoroutine(StertchStringTo0());
        }
        IEnumerator StertchStringTo0()
        {
            float a = 0.1f;
            string text = hintText.text;
            for(float i = 0; 1+i+a*i-(i*i) > 0; i+=Time.deltaTime/Time.timeScale*2)
            {
                hintText.text = TextA.Stretch(text,1 + i + a * i - i * i);
                hintText2.text = hintText.text;
                yield return new WaitForEndOfFrame();
            }
            hintText.text = "";
            hintText2.text = "";
        }
    }
    private static bool isGlassMoves;
    public static IEnumerator MoveGlassToPos(Vector3 pos,float time,bool isChecksNextFrame,bool UI)
    {
        if(!UI) pos = Camera.main.ScreenToWorldPoint(pos);
        glassColor = Color.white;
        Vector3 vector = Vector3.zero;
        pos.z = 0;
        if(isChecksNextFrame)
        {
            yield return new WaitForFixedUpdate();
            if(isGlassMoves) yield break;
        }
        isLookingForReferences = false;
        isGlassMoves = true;
        for(float i = time; i > 0; i -= Time.deltaTime/Time.timeScale)
        {
            SetGlassPos(Vector3.SmoothDamp(UI ? GlassUI.position : Glass.position,pos,ref vector,i,99999,Time.deltaTime / Time.timeScale),UI);
            yield return new WaitForEndOfFrame();
        }
        isGlassMoves=false;
        yield return new WaitForSecondsRealtime(1.7f);
        while(!isGlassMoves&&glassRenderer.color.a>0)
        {
            glassColor = new(glassRenderer.color.r,glassRenderer.color.g,glassRenderer.color.b,glassRenderer.color.a-Time.deltaTime / Time.timeScale * 0.8f);
            yield return new WaitForEndOfFrame();
        }
        GlassUI.position = new Vector3(-999,-999);
    }
}
