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
    public string Name { get; set; }
    public string Desc { get; set; }

    private void Start()
    {
        Count = PlayerPrefs.GetInt(_key, 0);
        _itemSprite = transform.GetChild(0).GetComponentInChildren<Image>();
        _countText = transform.GetChild(1).GetComponent<Text>();
        _descText = transform.GetChild(2).GetComponent<Text>();
        _nameText = transform.GetChild(0).GetComponentInChildren<Text>();


        IsOpen = Count > 0;
        SetData();
    }

    protected virtual void SetData()
    {
        SetSprite(_itemSprite,_sprite,IsOpen ? Color.white : Color.black);

        SetText(_descText,HideText(Desc));
        SetText(_nameText,HideText(Name));
        SetText(_countText,MathA.NumberToString(Count));
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
    protected string HideText(string text) => IsOpen ? text : "?".Stretch(text.Length);
}