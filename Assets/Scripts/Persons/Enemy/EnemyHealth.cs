using UnityEngine;

public class EnemyHealth : DefaultHealth, IScannable
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
        me._PC.GetComponentInChildren<PC>().EnemyKilled();
        Destroy(gameObject);
    }

    protected override void FixedUpdate()
    {
    }

    public void StartScan()
    {
    }

    public void StopScan()
    {
    }

    public void EndScan()
    {
        ApplyDamage(9999);
    }
}