using DustyStudios.MathAVM;

using UnityEngine;

public class ColliderOffsetPLUS : MonoBehaviour
{
    new Collider2D collider;
    SpriteRenderer spriteRenderer;
    Vector2 defaultColliderOffset;
    private void Awake()
    { 
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        defaultColliderOffset = collider.offset;
    }
    void FixedUpdate()
    {
        collider.offset = defaultColliderOffset * (Vector2.right * MathA.OneOrNegativeOne(spriteRenderer.flipX) + Vector2.up * MathA.OneOrNegativeOne(spriteRenderer.flipY));
    }
}
