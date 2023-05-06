using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float Health;
    [SerializeField] private int Damage;
    [SerializeField] private int DamageForWall;
    private GameObject PC;
    private GameObject _GameManager;
    private Rigidbody2D rb;
    private Animator anim;
    public GameObject peenaemuy;
   [SerializeField] private Transform peeneytut;

    private void Start()
    {
      
        rb = GetComponent<Rigidbody2D>();
        PC = GameObject.FindGameObjectWithTag("PC");
        _GameManager = GameObject.FindGameObjectWithTag("GameManager");
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        EnemyMove();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PC"))
        {
            _GameManager.GetComponent<GameManager>().TakeDamage(Damage);
            Destroy(gameObject);
        }
        if (other.CompareTag("AntivirusAtack") || other.CompareTag("ATACK EVERYBODY"))
        {
            //TakeDamage(other.GetComponent<AtackProjectile>().power);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
      
        if (other.gameObject.CompareTag("PC"))
        {
            _GameManager.GetComponent<GameManager>().TakeDamage(Damage);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            other.gameObject.GetComponent<Wall>().TakeDamageWall(DamageForWall);
        }
      
    }
    private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("Enemy"))
        { 
           anim.SetTrigger("peenok");
           
            peenaemuy=other.gameObject;
        }
    }
    public void TakeDamage(float Damage)
    {
        Health -= Damage;
        if (Health <= 0)
        {
            _GameManager.GetComponent<GameManager>().TakeEXP(1);
            Speed = 0;
            anim.SetTrigger("Die");
        }
    }

    public void AfterDie()
    {
        Destroy(gameObject);
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
    public void stun()
    {
        if (peenaemuy != null)
        {
            peenaemuy.transform.position = peeneytut.position;
        }
    }
    public void peenok()
    {
        if (peenaemuy != null)
        {
            peenaemuy.GetComponent<Rigidbody2D>().AddForce(peenaemuy.transform.position / 2);
        }
    }
}
