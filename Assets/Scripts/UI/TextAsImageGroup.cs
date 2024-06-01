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
        get => text; set
        {
            text = value;
            PrintText();
            ArrangeChildren();
        }
    }

    List<RectTransform> Characters = new();
    void Update() => ArrangeChildren();
    void OnValidate()
    {
        EditorApplication.delayCall += () => PrintText();
        ArrangeChildren();
    }

    private void PrintText()
    {
        foreach(var t in Characters)
            if(t!=null) DestroyImmediate(t.gameObject);
        Characters.Clear();
        for(int i = rectTransform.childCount; i > 0;)
            DestroyImmediate(rectTransform.GetChild(--i).gameObject);
        Dictionary<char,RectTransform> letterDict = new();
        Dictionary<RectTransform,int> rectIndeces = new();
        string Text = this.Text;
        foreach(var character in font.List)
            letterDict[character.letter] = character.gameObject;
        foreach(var strin in font.ListOfStrings)
        {
            if(Text.Contains(strin.String))
            {
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
                    rectIndeces.Add(symbol,indices[i-1]);
                    symbol.name = strin.String + " Symbol";
                }
                Text = Text.Replace(strin.String,new string(strin.String.Select(c => '\u0010').ToArray()));
            }
        }
        foreach(char character in Text)
        {
            if(character == '\u0010') Characters.Add(null);
            else if(letterDict.ContainsKey(character))
            {
                Characters.Add(Instantiate(letterDict[character],rectTransform));
                Characters.Last().name = character + " Symbol";
            }
            else print($"Font has'nt character for {character}.");
        }
        foreach(var rectIndex in rectIndeces)
            Characters[rectIndex.Value] = rectIndex.Key;
        Characters = Characters.Where(c => c!=null).ToList();
        for(int i = 0; i < Characters.Count; i++)
            Characters[i].SetSiblingIndex(i);
    }

    void ArrangeChildren()
    {
        if(transform.childCount == 0) return;
        RectTransform childRect = RectTransform.GetChild(0) as RectTransform;
        Vector2 current=new(FirstX(childRect),
                            childRect.rect.height*-0.5f);
        int childCount = rectTransform.childCount;
        for(int i = 0; i < childCount;)
        {
            childRect = rectTransform.GetChild(i++) as RectTransform;
            float width = childRect.rect.width;
            if(current.x+width/2 >= rectTransform.rect.width)
            {
                current.y -= childRect.rect.height + spacing;
                current.x = FirstX(childRect);
            }
            childRect.anchoredPosition = current;
            if(i < childCount) width = (width + (rectTransform.GetChild(i) as RectTransform).rect.width)/2;
            current.x += width + spacing;
        }

        float FirstX(RectTransform child)=>child.rect.width / 2;
    }
    
}