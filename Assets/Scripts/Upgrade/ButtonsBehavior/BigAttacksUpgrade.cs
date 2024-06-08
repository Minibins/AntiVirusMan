using UnityEngine;
public class BigAttacksUpgrade : Upgrade
{
    protected override void OnTake()
    {
        base.OnTake();
        GameObject.FindObjectOfType<PlayerAttack>().Damage.additions.Add(3);
        GameObject.FindObjectOfType<PlayerStats>().Recharging.multiplingMultiplers.Add(0.5f);
        Level.EnemyNeedToUpLVL /= 1.5f;
    }
}
