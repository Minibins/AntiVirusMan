using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StepaHedgehog : SpeedBoost
{
    private Enemy me;
    private new Transform transform;
    private LayerMask maskWhoKills;
    [SerializeField] private GameObject ring;
    bool isRunning = true;
    float speed;
    bool IsRunning
    {
        set 
        {
            me._move.CanJump = value;
            me._move.ClearSpeedMultiplers();
            me._move.SetSpeedMultiplierForever(value ? 1f : 0f);
            isRunning = value;
            _animator.SetBool("IsRunning",value);
        }
    }
    protected override void Start()
    {
        me = GetComponent<Enemy>();
        speed = me._move._speed;
        IsRunning = true;
        maskWhoKills = me._maskWhoKills;
        me._maskWhoKills = 0;
        base.Start();
        SetTarget();
        transform = base.transform;
    }

    private void SetTarget()
    {
        IOrderedEnumerable<Enemy> targets = Enemy.Enemies.OfType<Enemy>().Where(e =>
            (e.WhoAmI == EnemyTypes.Soplik || e.WhoAmI == EnemyTypes.Booka) && 
            (!(e is WireEnemy) || (e as WireEnemy).isDefault) &&
            !e.GetComponent<DebuffBank>().HasDebuffOfType(typeof(Speeding))).OrderBy(e => Random.Range(0, 10));
        if (targets.ToArray().Length > 0)
            me._PC = targets.LastOrDefault().gameObject;
        else
        {
            me.ResetPC();
            GetComponent<AttackProjectile>().Damage = 2;
            me._maskWhoKills = maskWhoKills;
        }
    }

    private void FixedUpdate()
    {
        if (me._PC == null || me._PC.IsDestroyed())
        {
            SetTarget();
        }
    }
    protected override bool EnterCondition(Collider2D other) => base.EnterCondition(other) && isRunning;
    protected override IEnumerator AuraAction()
    {
        Instantiate(ring, transform.position, Quaternion.identity);
        IsRunning = false;
        yield return base.AuraAction();
        SetTarget();
        yield return new WaitForSeconds(3f);
        IsRunning = true;
    }
}