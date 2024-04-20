using DustyStudios.MathAVM;
using UnityEngine;

public class ColliderOffsetPLUS : MonoBehaviour
{
    private new Collider2D collider;
    private SpriteRenderer spriteRenderer;
    private Vector2 defaultColliderOffset;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        defaultColliderOffset = collider.offset;
    }

    private void FixedUpdate()
    {
        collider.offset = defaultColliderOffset * (Vector2.right * MathA.OneOrNegativeOne(spriteRenderer.flipX) +
                                                   Vector2.up * MathA.OneOrNegativeOne(spriteRenderer.flipY));
    }
}