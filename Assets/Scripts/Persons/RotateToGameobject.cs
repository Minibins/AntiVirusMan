using UnityEngine;
using UnityEngine.EventSystems;

public class RotateToGameobject : MonoBehaviour
{
    [SerializeField] bool X, Y;
    new private Transform transform;
    public Transform Gameobject;
    SpriteRenderer spriteRenderer;
    void Awake()
    {
        transform = base.transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if(X) spriteRenderer.flipX = Gameobject.position.x > transform.position.x;
        if(Y) spriteRenderer.flipY = Gameobject.position.y > transform.position.y;
    }
}
