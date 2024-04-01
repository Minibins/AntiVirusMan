using UnityEngine;

public class EnemyHealth : DefaultHealth
{
    private Enemy me;
    private Transform player;

    protected override void Start()
    {
        base.Start();
        me = GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void ApplyDamage(float damage)
    {
        if (LevelUP.Items[13].IsTaken == true)
        {
            if (me.moveDirection == -1f && player.position.x > transform.position.x ||
                me.moveDirection == 1f && player.position.x < transform.position.x)
            {
                CurrentHealth -= damage;
            }
        }

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