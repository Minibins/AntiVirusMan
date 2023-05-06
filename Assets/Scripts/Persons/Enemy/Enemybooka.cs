using UnityEngine;
public class Enemybooka : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float Health;
    [SerializeField] private int Damage;
    [SerializeField] private int DamageForWall;
    private GameObject PC;
    private GameObject _GameManager;
    private Rigidbody2D rb;
    private Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        PC = GameObject.FindGameObjectWithTag("PC");
        _GameManager = GameObject.FindGameObjectWithTag("GameManager");
    }
    private void FixedUpdate()
    {
        EnemyMove();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PC"))
        {
            if (Health > 0)
            {
                _GameManager.GetComponent<GameManager>().TakeDamage(Damage);
                Destroy(gameObject);
            }
        }
        if (other.CompareTag("AntivirusAtack") || other.CompareTag("ATACK EVERYBODY"))
        {
            TakeDamage(other.GetComponent<AtackProjectile>().power);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PC"))
        {
            _GameManager.GetComponent<GameManager>().TakeDamage(Damage);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Player") && other.transform.position.y > transform.position.y + 1)
        {
            TakeDamage(other.gameObject.GetComponent<PlayerAttack>().Damage * 3);
        }
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            other.gameObject.GetComponent<Wall>().TakeDamageWall(DamageForWall);
        }
    }
    public void TakeDamage(float Damage)
    {
        Health -= Damage;
        if (Health <= 0)
        {
            _GameManager.GetComponent<GameManager>().TakeEXP(1);
            anim.SetTrigger("zadavil");
            Damage = 0;
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
    }
}