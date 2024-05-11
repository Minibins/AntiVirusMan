using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu]
public class JustSprite : ScriptableObject, ISprite
{
    [SerializeField] private Sprite _sprite;

    public JustSprite(Sprite sprite)
    {
        _sprite = sprite;
    }

    public Sprite Sprite => _sprite;

    public void OnSpriteApplied(SpriteRenderer renderer,Image image, CustomRendererSpriteChanger changer)
    {
        DustyStudios.DustyConsole.Print($"Sprite {_sprite.name} has applied to renderer of {renderer.gameObject.name}");
    }

    public void OnSpriteRemoved(SpriteRenderer renderer,Image image,CustomRendererSpriteChanger changer)
    {
        DustyStudios.DustyConsole.Print($"Sprite {_sprite.name} has removed from renderer of {renderer.gameObject.name}");
    }
}
