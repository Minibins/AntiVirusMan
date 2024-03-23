using System.Collections;
using System.Linq;

using Unity.VisualScripting;

using UnityEngine;

public class StepaHedgehog : SpeedBoost
{
    Enemy me;
    new Transform transform;
    LayerMask maskWhoKills;
    [SerializeField] GameObject ring;
    protected override void Start()
    {
        me = GetComponent<Enemy>();
        maskWhoKills = me._maskWhoKills;
        me._maskWhoKills = 0;
        base.Start();
        SetTarget();
        transform = base.transform;
    }

    private void SetTarget()
    {
        IOrderedEnumerable<Enemy> targets = Enemy.Enemies.Where(e =>( e.WhoAmI == EnemyTypes.Soplik || e.WhoAmI == EnemyTypes.Booka )&& e.ChangeMove==0 && 
        !e.Move.IsMultiplierBoost()).OrderBy(e=>Random.Range(0,10));
        if(targets.ToArray().Length > 0)
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
        if(me._PC == null || me._PC.IsDestroyed()) 
        {
            SetTarget();
        }
    }
    protected override IEnumerator AuraAction()
    {
        Instantiate(ring, transform.position,Quaternion.identity);
        yield return base.AuraAction();
        SetTarget();
    }
}