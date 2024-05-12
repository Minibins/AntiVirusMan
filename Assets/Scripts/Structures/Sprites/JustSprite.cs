using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu]
public class JustSprite : SpriteContainingSO
{
    [SerializeField] private Sprite _sprite;

    public JustSprite(Sprite sprite)
    {
        _sprite = sprite;
    }

    public override Sprite Sprite => _sprite;

    public override void OnSpriteApplied(SpriteRenderer renderer,Image image,CustomRendererSpriteChanger changer)
    {
    }

    public override void OnSpriteRemoved(SpriteRenderer renderer,Image image,CustomRendererSpriteChanger changer)
    {
    }
}
