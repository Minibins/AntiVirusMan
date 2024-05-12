using UnityEngine.UI;
using UnityEngine;

public class CustomRendererSpriteChanger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Image imageRenderer;
    private SpriteContainingSO sprite;
    public void SetSpriteSo(SpriteContainingSO value)
    {
        SpriteRenderer = value.Sprite;
        sprite?.OnSpriteRemoved(spriteRenderer,imageRenderer,this);
        value.OnSpriteApplied(spriteRenderer,imageRenderer,this);
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
                if(spriteRenderer == null && !TryGetComponent<SpriteRenderer>(out spriteRenderer)) hasSpriteRenderer = false;
                else spriteRenderer.sprite = value;
            }
            if(hasImage)
            {
                if(imageRenderer == null && !TryGetComponent<Image>(out imageRenderer)) hasImage = false;
                else imageRenderer.sprite = value;
            }
        }
    }
}