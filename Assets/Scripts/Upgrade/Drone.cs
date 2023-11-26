using System.Collections;
using System.Collections.Generic;

using MathAVM;

using Unity.Mathematics;

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
            if(rb.velocity.x < 0)
            {
                sprite.sortingOrder = 1001;
            }
            else
            {
                sprite.sortingOrder = 997;
            }
       if(Grounded.IsGrounded()) Following(playerPosition.position+Vector3.up*howMuchUp,true,transform);
    }
    public void OnDamageGet(int Damage)
    {
        
        anim.SetTrigger("Attack");
        damage = Damage;
    }
    public void Shoot(float rotation)
    {
        rotation = rotation % 180;
        for (int i = 0; i < 2; i++)
        {
            GameObject bullet = Instantiate(bulletAsset,transform.position,transform.rotation);
            AttackProjectile bulletAttack=bullet.GetComponent<AttackProjectile>();
            Vector2 velocity = bulletAttack._velosity;
                
            
            switch(i)
            {
                case 1:
                rotation *= -1;
                
                
                break;
            }
            bullet.GetComponent<SpriteRenderer>().flipY = true;
            bulletAttack.Damage = damage;
            bulletAttack._velosity = MathA.RotatedVector(velocity,rotation);
            if(bulletAttack._velosity.y>0) bulletAttack._velosity*=-1;
            
            bulletAttack._mask.value = 23552;
            bullet.transform.rotation = MathA.VectorsAngle(bulletAttack._velosity);
        }

    }
}
