using UnityEngine;

public class NuclearBomb : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private Vector2 startvelocity;
    [SerializeField] private Rigidbody2D rb;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = startvelocity;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
        {
            explode();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
        {
            explode();
        }
    }
    void explode()
    {
        GameObject xplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        //xplosion.GetComponent<AtackProjectile>().power = 0.5f;
        Destroy(gameObject);
    }
    private void Update()
    {
        transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z + 1 / rb.velocity.y / 5, transform.rotation.w);
    }
}
