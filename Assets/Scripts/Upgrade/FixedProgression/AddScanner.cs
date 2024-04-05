using UnityEngine;

public class AddScanner : FixedProgressionUpgrade
{
    public override void Pick()
    {
        base.Pick();
        GetComponentInParent<Animator>().SetBool("CanScan", true);
        GetComponentInParent<PlayerAttack>().stopAttackOnAnimationEnd = false;
    }
}
