using UnityEngine;

public class UpgradeStatsItem : StatsItem
{
    [SerializeField] private Sprite unknowSprite;

    public Sprite UnknowSprite
    {
        get => unknowSprite;
        set => unknowSprite = value;
    }

    public Sprite Sprite
    {
        get => _sprite;
        set
        {
            _key = _sprite.name;
            _sprite = value;
            SetSprite();
        }
    }

    private void Awake()
    {
        if (_key.Length < 1)
            _key = _sprite.name;
    }


    protected override void SetSprite()
    {
        if (_count <= 0)
            _itemSprite.sprite = UnknowSprite;
        else
            base.SetSprite();
    }
}