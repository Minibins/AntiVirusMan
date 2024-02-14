using UnityEngine;
public class NuclearBomb : ExplodeAndDeath, IExplosion
{
    [SerializeField] Vector2 startvelocity;
    Rigidbody2D rb;
    float radius;
    int power;
    public float Radius { set => radius = value; }
    public int Power { set => power = value; }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = startvelocity;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        OnCollisionEnter2D(other);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy")&&!other.CompareTag("Player"))
        {
            Action();
        }
    }
    private void Update()
    {
        transform.rotation = Quaternion.Euler(0,0,rb.velocity.y*(180/startvelocity.y));
    }
}