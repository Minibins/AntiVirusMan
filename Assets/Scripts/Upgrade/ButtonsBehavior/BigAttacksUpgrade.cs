using UnityEngine;
public class BigAttacksUpgrade : Upgrade
{
    protected override void OnTake()
    {
        base.OnTake();
        PlayerAttack attacke = GameObject.FindObjectOfType<PlayerAttack>();
        attacke.Damage.additions.Add(3);
        attacke._timeReload /= 2;
        Level.EnemyNeedToUpLVL /= 1.5f;
    }
}
