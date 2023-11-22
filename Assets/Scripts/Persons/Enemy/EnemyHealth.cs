using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    Enemy me;
    Transform player;
    protected override void Start()
    {
        base.Start();
        me = GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public override void ApplyDamage(float damage)
    {
        if(LevelUP.isTaken[13] == true)
        {
            if(me.moveDirection == -1f && player.position.x > transform.position.x ||
                me.moveDirection == 1f && player.position.x < transform.position.x)
            {
                CurrentHealth -= damage;
            }

        }
        base.ApplyDamage(damage);
    }
    public override void DestroyHimself()
    {
        me._PC.GetComponentInChildren<PC>().EnemyKilled();
        Destroy(gameObject);
        
    }
}
