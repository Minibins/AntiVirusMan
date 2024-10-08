using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
[ExecuteInEditMode]
public class TextAsImageGroup : MonoBehaviour
{
    [SerializeField] private string text;
    public float spacing = 1f;
    [SerializeField] private PrefabFont font;
    RectTransform rectTransform;
    public RectTransform RectTransform 
    { 
        get
        {
            if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
            return rectTransform;
        }
    }

    public string Text
    {
        get => text; 
        set
        {
            if(text == value) return;

            text = value;
            PrintText();
            ArrangeChildren();
        }
    }

    List<RectTransform> Characters = new();
    void Update() => ArrangeChildren();
    #if UNITY_EDITOR
    void OnValidate()
    {
        EditorApplication.delayCall += () =>
        {
            PrintText();
            ArrangeChildren();
        };
    }
#endif
    private void PrintText()
    {
        foreach(var t in Characters)
            if(t != null) DestroyAnyWay(t.gameObject);
        Characters.Clear();
        for(int i = RectTransform.childCount; i > 0;)
            DestroyAnyWay(RectTransform.GetChild(--i).gameObject);
        Dictionary<char,RectTransform> letterDict = new();
        Dictionary<RectTransform,int> rectIndeces = new();
        string Text = this.Text;
        foreach(var character in font.List)
            letterDict[character.letter] = character.gameObject;
        foreach(var strin in font.ListOfStrings)
        {
            if(!Text.Contains(strin.String))
                continue;
            List<int> indices = new List<int>();
            int index = this.Text.IndexOf(strin.String);
            while(index != -1)
            {
                indices.Add(index);
                index = this.Text.IndexOf(strin.String,index + strin.String.Length);
            }
            for(int i = Text.Split(strin.String).Length - 1; i > 0; i--)
            {
                RectTransform symbol = Instantiate(strin.gameObject,RectTransform);
                Characters.Add(symbol);
                rectIndeces.Add(symbol,indices[i - 1]);
                symbol.name = strin.String + " Symbol";
            }
            Text = Text.Replace(strin.String,new string(strin.String.Select(c => '\u0010').ToArray()));
        }
        foreach(char character in Text)
        {
            if(character == '\u0010') Characters.Add(null);
            else if(letterDict.ContainsKey(character))
            {
                Characters.Add(Instantiate(letterDict[character],RectTransform));
                Characters.Last().name = character + " Symbol";
            }
            else print($"Font has'nt character for {character}.");
        }
        foreach(var rectIndex in rectIndeces)
            Characters[rectIndex.Value] = rectIndex.Key;
        Characters = Characters.Where(c => c != null).ToList();
        for(int i = 0; i < Characters.Count; i++)
            Characters[i].SetSiblingIndex(i);
    }
    void DestroyAnyWay(GameObject obj)
    {
        if(Application.isEditor && !Application.isPlaying)
            DestroyImmediate(obj);
        else
            Destroy(obj);
    }
    void ArrangeChildren()
    {
        if(RectTransform.childCount == 0) return;
        RectTransform childRect = RectTransform.GetChild(0) as RectTransform;
        Vector2 current=new(FirstX(childRect),
                            childRect.rect.height*-0.5f);
        int childCount = RectTransform.childCount;
        for(int i = 0; i < childCount;)
        {
            childRect = RectTransform.GetChild(i++) as RectTransform;
            float width = childRect.rect.width;
            if(current.x+width/2 >= RectTransform.rect.width)
            {
                current.x = FirstX(childRect);
                current.y -= childRect.rect.height + spacing;
                if(-current.y > RectTransform.rect.height) current.y = -9999;
            }
            childRect.anchoredPosition = current;
            if(i < childCount) width = (width + (RectTransform.GetChild(i) as RectTransform).rect.width)/2;
            current.x += width + spacing;
        }

        float FirstX(RectTransform child)=>child.rect.width / 2;
    }
    
}