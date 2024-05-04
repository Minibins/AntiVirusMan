using System;
using UnityEngine;
using UnityEngine.UI;

public class EggItem : EggStatsItem
{
    [SerializeField] private String _text;
    [SerializeField] private Image _mainSprite;
    [SerializeField] private Text _mainText;

    public override void SetInfromation()
    {
        _mainSprite.color = _count > 0 ? Color.white : Color.black;
        _mainSprite.sprite = _itemSprite.sprite;
        _mainText.text = _text;
    }
}
