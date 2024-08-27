using System.Collections;

using UnityEngine;
using UnityEngine.UI;

using DustyStudios.TextAVM;
using DustyStudios.MathAVM;
public class StatsItem : MonoBehaviour
{
    [SerializeField] protected string _key;
    [SerializeField] protected Sprite _sprite;
    public int Count { get; private set; }
    public bool IsOpen { get; private set; }
    protected Image _itemSprite;
    protected Text _nameText, _descText, _countText;
    public string Name 
    { 
        get; 
        set; 
    }
    public string Desc
    {
        get; 
        set;
    }
    bool hadStart;
    protected virtual void Start()
    {
        _itemSprite = transform.GetChild(0).GetComponentInChildren<Image>();
        _countText = transform.GetChild(1).GetComponent<Text>();
        _descText = transform.GetChild(2).GetComponent<Text>();
        _nameText = transform.GetChild(0).GetComponentInChildren<Text>();

        SetCount();
        IsOpen = Count > 0;
        SetData();
        hadStart = true;
    }

    private void OnEnable()
    {
        if(hadStart) SetData();
    }

    protected virtual void SetData()
    {
        if(
            Desc == null || 
            Name == null || 
            _sprite == null)
        {
            Invoke(nameof(SetData),Time.deltaTime);
            return;
        }
        SetText(_descText,HideText(Desc));
        SetText(_nameText,HideText(Name));
        SetSprite(_itemSprite,_sprite,IsOpen ? Color.white : Color.black);
    }

    private void SetCount()
    {
        Count = PlayerPrefs.GetInt(_key,0);
        string countText = MathA.NumberToString(Count);
        if(countText != Count.ToString()) countText += "\n(" + Count + ")";
        SetText(_countText,countText);
    }

    protected virtual void SetSprite(Image image, Sprite sprite, Color color)
    {
        image.sprite = sprite;
        image.color = color;
    }

    protected virtual void SetText(Text text,string value)
    {
        if(text != null)
        text.text = value;
    }
    protected string HideText(string text)
    {
        if(text == null)return "?";
        else            return IsOpen ? text : "?".Stretch(text.Length);
    }
}