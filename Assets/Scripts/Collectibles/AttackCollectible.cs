using UnityEngine;

public class AttackCollectible : HoldCollectible
{
    public override void Pick(GameObject picker)
    {
        base.Pick(picker);
        GameObject.FindObjectOfType<PlayerAttack>().TemporaryAttackSubstitute = GetComponent<AbstractAttack>();
    }
}
