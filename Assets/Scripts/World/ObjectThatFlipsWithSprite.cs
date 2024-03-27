using DustyStudios.MathAVM;

using UnityEngine;

public class ObjectThatFlipsWithSprite : MonoBehaviour
{
    [SerializeField] SpriteRenderer _sprite;
    new Transform transform;
    private void Start()
    {
        transform = base.transform;
    }
    private void LateUpdate()
    {
        transform.localScale = Vector3.one-Vector3.right+Vector3.right*MathA.OneOrNegativeOne(!_sprite.flipX);
    }
}
