using UnityEngine;

public class EnemyHealth : DefaultHealth
{
    private AbstractEnemy me;
    private Transform player;

    protected override void Start()
    {
        base.Start();
        me = GetComponent<AbstractEnemy>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void ApplyDamage(float damage)
    {
        if(LevelUP.IsItemTaken(13) == true && (me.moveDirection > 0 ^ player.position.x > transform.position.x) && me.moveDirection!=0)
            CurrentHealth -= damage * multiplerDamage;
        base.ApplyDamage(damage);
    }

    public override void DestroyHimself()
    {
        Destroy(gameObject);
        me._PC.GetComponentInChildren<PC>().EnemyKilled();
    }

    protected override void FixedUpdate()
    {
    }
}