using UnityEngine;
public abstract class SpriteContainingSO : ScriptableObject
{
    public abstract Sprite Sprite { get; }
    public abstract void OnSpriteApplied(SpriteRenderer renderer,CustomRendererSpriteChanger changer);
    public abstract void OnSpriteRemoved(SpriteRenderer renderer,CustomRendererSpriteChanger changer);
}
