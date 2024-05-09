using UnityEngine;
[CreateAssetMenu]
public class JustSprite : SpriteContainingSO
{
    [SerializeField] private Sprite _sprite;

    public JustSprite(Sprite sprite)
    {
        _sprite = sprite;
    }

    public override Sprite Sprite => _sprite;

    public override void OnSpriteApplied(SpriteRenderer renderer,CustomRendererSpriteChanger changer)
    {
        DustyStudios.DustyConsole.Print($"Sprite {_sprite.name} has applied to renderer of {renderer.gameObject.name}");
    }

    public override void OnSpriteRemoved(SpriteRenderer renderer,CustomRendererSpriteChanger changer)
    {
        DustyStudios.DustyConsole.Print($"Sprite {_sprite.name} has removed from renderer of {renderer.gameObject.name}");
    }
}
