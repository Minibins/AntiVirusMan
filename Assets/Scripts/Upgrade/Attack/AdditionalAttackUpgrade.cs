using UnityEngine;

public class AdditionalAttackUpgrade : Upgrade
{
    [SerializeField] AbstractAttack abstractAttack;
    protected override void OnTake()
    {
        base.OnTake();
        GameObject.FindObjectOfType<PlayerAttack>().AdditionalAttacks.Add(abstractAttack);
    }
}
