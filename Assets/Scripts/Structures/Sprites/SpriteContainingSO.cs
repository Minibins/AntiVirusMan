using UnityEngine;
using UnityEngine.UI;
public abstract class SpriteContainingSO : ScriptableObject
{
    public abstract Sprite Sprite { get; }
    public abstract void OnSpriteApplied(SpriteRenderer renderer,Image image,CustomRendererSpriteChanger changer);
    public abstract void OnSpriteRemoved(SpriteRenderer renderer,Image image,CustomRendererSpriteChanger changer);
}
