using UnityEngine;

public class ForceAround : MonoBehaviour
{
    [SerializeField] float radius = 5, force = 10, distanceAffect = 1;
    new Transform transform;
    private void Awake() => transform = base.transform;
    public void Explode()
    {
        Vector2 explosionPosition = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPosition, radius);

        foreach(Collider2D collider in colliders)
        {
            Rigidbody2D rb;
            if(collider.TryGetComponent(out rb))
            {
                Vector2 rbPosition = rb.position;
                Vector2 direction = (rbPosition - explosionPosition).normalized;
                rb.AddForce(direction * (force - Vector2.Distance(explosionPosition, rbPosition)*distanceAffect),ForceMode2D.Impulse);
            }
        }
    }
}
