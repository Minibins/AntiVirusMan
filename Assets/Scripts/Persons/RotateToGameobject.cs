using UnityEngine;

public class RotateToGameobject : MonoBehaviour
{
    [SerializeField] private bool X, Y;
    private new Transform transform;
    public Transform Gameobject;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        transform = base.transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (X) spriteRenderer.flipX = Gameobject.position.x > transform.position.x;
        if (Y) spriteRenderer.flipY = Gameobject.position.y > transform.position.y;
    }
}