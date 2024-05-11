using UnityEngine.UI;
using UnityEngine;

public class CustomRendererSpriteChanger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Image imageRenderer;
    private ISprite sprite;
    public void SetSpriteSo(ISprite value)
    {
        SpriteRenderer = value.Sprite;
        value.OnSpriteApplied(spriteRenderer,imageRenderer,this);
        sprite?.OnSpriteRemoved(spriteRenderer,imageRenderer,this);
        sprite = value;
    }
    public void SetSprite(Sprite value)
    {
        SpriteRenderer = value;
        sprite?.OnSpriteRemoved(spriteRenderer,imageRenderer,this);
        sprite = null;
    }
    bool hasSpriteRenderer = true, hasImage = true;
    public Sprite SpriteRenderer 
    {
        set
        {
            if(hasSpriteRenderer)
            {
                if(spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
                if(spriteRenderer == null) hasSpriteRenderer = false;
                spriteRenderer.sprite = value;
            }
            if(hasImage)
            {
                if(imageRenderer == null) imageRenderer = GetComponent<Image>();
                if(imageRenderer == null) hasImage = false;
                imageRenderer.sprite = value;
            }
        }
    }
}