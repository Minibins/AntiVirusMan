using UnityEngine;
using UnityEngine.UI;

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
            SetData();
        }
    }

    override protected void Start()
    {
        if (_key.Length < 1)
            _key = _sprite.name;
        base.Start();
    }


    protected override void SetSprite(Image image,Sprite sprite,Color c) => base.SetSprite(image,IsOpen ? sprite : unknowSprite,Color.white);
}