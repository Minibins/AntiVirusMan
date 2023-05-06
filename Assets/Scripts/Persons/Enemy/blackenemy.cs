using UnityEngine;
public class blackenemy : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private float Speed;
    [SerializeField] private float Health;
    [SerializeField] private int Damage;
    [SerializeField] private int pcnormally;
    private Animator anim;
    private bool explode;
    private GameObject PC;
    private GameObject _GameManager;
    private Rigidbody2D rb;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, -1f, transform.position.z);
        anim = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        PC = GameObject.FindGameObjectWithTag("PC");
        _GameManager = GameObject.FindGameObjectWithTag("GameManager");
    }
    private void FixedUpdate()
    {
        if (!explode) { EnemyMove(); }
        else { fallAndExplode(); }
        if (transform.position.y < pcnormally)
        {
            Instantiate(explosion, new Vector3(transform.position.x, -2.05f, transform.position.z), Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("AntivirusAtack") || other.CompareTag("ATACK EVERYBODY"))
        {
            //TakeDamage(other.GetComponent<AtackProjectile>().power);
        }
    }

    public void TakeDamage(float Damage)
    {
        Health -= Damage;
        if (Health <= 0)
        {
            explode = true;
            rb.velocity = new Vector2(rb.velocity.x, 10);
        }
    }
    private void EnemyMove()
    {
        if (PC.transform.position.x < transform.position.x&& rb.velocity != new Vector2(-Speed, 0))
        {
            rb.velocity = new Vector2(-Speed, 0);
            transform.localScale = new Vector2(6f, 6f);
        }
        else if (PC.transform.position.x > transform.position.x&& rb.velocity != new Vector2(Speed, 0))
        {
            rb.velocity = new Vector2(Speed, 0);
            transform.localScale = new Vector2(-6f, 6f);
        }
        if (PC.transform.position.x < transform.position.x + 1 && PC.transform.position.x > transform.position.x - 1)
        {
            explode = true;
            rb.velocity = new Vector2(rb.velocity.x, 10);
        }
    }
    public void fallAndExplode()
    {
        rb.gravityScale = 2;
    }
}