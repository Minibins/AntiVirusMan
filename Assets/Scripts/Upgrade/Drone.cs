using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : Follower,IDamageble
{
    private SpriteRenderer sprite;
    [SerializeField] float howMuchUp;
    [SerializeField] GameObject bulletAsset;
    private int damage;
    private Animator anim;
    private Player Grounded;
    override private protected void Start()
    {
        anim = GetComponent<Animator>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        rb=GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        Grounded=playerPosition.GetComponent<Player>();
    }
    override private protected void Update()
    {
        if(!FarPlayer(playerPosition.position + Vector3.up * howMuchUp,transform))
        {
            if(rb.velocity.x < 0)
            {
                sprite.sortingOrder = 1001;
            }
            else
            {
                sprite.sortingOrder = 997;
            }
        }
        if(Grounded.IsGrounded())Following(playerPosition.position+Vector3.up*howMuchUp,true,transform);
    }
    public void OnDamageGet(int Damage)
    {
        
        anim.SetTrigger("Attack");
        damage = Damage;
    }
    public void Shoot(float rotation)
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject bullet = Instantiate(bulletAsset,transform.position,transform.rotation);
            AttackProjectile bulletAttack=bullet.GetComponent<AttackProjectile>();
            Vector3 velocity = bulletAttack._velosity;
            switch(i)
            {
                case 0:
                    bullet.transform.rotation = Quaternion.Euler(180,0,rotation--);
                    
                break;
                case 1:
                bullet.transform.rotation = Quaternion.Euler(180,0,rotation--);
                
                break;
            }
            bullet.GetComponent<AttackProjectile>().Damage = damage;
            bulletAttack._velosity = new Vector3(velocity.x * Mathf.Cos(rotation) - velocity.y / Mathf.Sin(rotation),
                                                         velocity.x * Mathf.Sin(rotation) + velocity.y / Mathf.Cos(rotation)).normalized * 10;
            bulletAttack._mask.value=//Завтра сделаю
        }
       
    }
}
