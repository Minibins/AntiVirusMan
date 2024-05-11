using UnityEngine;
using UnityEngine.UI;
public interface ISprite 
{
    public Sprite Sprite { get; }
    public void OnSpriteApplied(SpriteRenderer renderer,Image image, CustomRendererSpriteChanger changer);
    public void OnSpriteRemoved(SpriteRenderer renderer,Image image, CustomRendererSpriteChanger changer);
}
