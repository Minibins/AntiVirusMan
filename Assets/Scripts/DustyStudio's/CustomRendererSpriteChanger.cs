using UnityEngine;

public class CustomRendererSpriteChanger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private SpriteContainingSO sprite;
    public SpriteContainingSO Sprite { get => sprite;
        set
        {
            SpriteRenderer.sprite = value.Sprite;
            value.OnSpriteApplied(SpriteRenderer,this);
            sprite?.OnSpriteRemoved(SpriteRenderer,this);
            sprite = value;
        }
    }

    public SpriteRenderer SpriteRenderer 
    {
        get
        {
            if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
            return spriteRenderer;
        }
    }
}