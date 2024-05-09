using UnityEngine;
[CreateAssetMenu]
public class SpriteReference : SpriteContainingSO
{
    [SerializeField] private Sprite _sprite;
    public override Sprite Sprite => _sprite;

    public override void OnSpriteApplied(SpriteRenderer renderer,CustomRendererSpriteChanger changer)
    { 
    }

    public override void OnSpriteRemoved(SpriteRenderer renderer,CustomRendererSpriteChanger changer)
    {   
    }
}
