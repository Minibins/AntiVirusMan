using System;
using UnityEngine;
using UnityEngine.UI;

public class EggItem : EggStatsItem
{
    [SerializeField] private string _text;
    [SerializeField] private Image _mainSprite;
    [SerializeField] private Text _mainText;

    public string Text { set => _text = value; }

    public override void SetInfromation()
    {
        _itemSprite = GetComponent<Image>();
        _itemSprite.color = _count > 0 ? Color.white : Color.black;
        _mainSprite.color = _count > 0 ? Color.white : Color.black;
        _mainSprite.sprite = _itemSprite.sprite;
        _mainText.text = _text;
    }
}
