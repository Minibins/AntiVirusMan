using UnityEngine;

    public class UpgradeStatsItem : StatsItem
    {
        [SerializeField] private Sprite unknowSprite;
        
        
        
        protected override void SetSprite()
        {
            base.SetSprite();
            if (_count <= 0)
            {
                _itemSprite.sprite = unknowSprite;
            }
        }
    }
