using UnityEngine;

public class AttackCollectible : HoldCollectible
{
    public override void Pick(GameObject picker)
    {
        base.Pick(picker);
        FindObjectOfType<PlayerAttack>().AddTemporaryAttackSubstitute(GetComponent<AbstractAttack>());
    }
}