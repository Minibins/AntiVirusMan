using UnityEngine;

public class UpgradeStatsItem : StatsItem
{
    [SerializeField] private Sprite unknowSprite;
    private void Awake()
    {
        _key = _sprite.name;
    }


    protected override void SetSprite()
    {
        if(_count <= 0)
        {
            _itemSprite.sprite = unknowSprite;
        }
        else
        {
            base.SetSprite();

        }
    }
}
