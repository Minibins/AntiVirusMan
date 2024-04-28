using UnityEngine;
public class MaskingSpriteRenderer : MonoBehaviour
{
    SpriteMask spriteMask;
    SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteMask = gameObject.AddComponent<SpriteMask>();
    }
    private void Update()
    {
        spriteMask.sprite = spriteRenderer.sprite;
    }
}
