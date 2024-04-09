using System.Collections;

using UnityEngine;

public class DummyHealth : Health
{
    [SerializeField] int TimeForRegeneration = 1;
    protected override void Awake()
    {
        base.Awake();
        OnApplyDamage += SetNextFinishing;
    }

    private void HealHealth()
    {
        base.HealHealth(1);
        animator.SetTrigger("Heal");
    }

    private void SetNextFinishing()
    {
        animator.SetTrigger("Finishing");
        Invoke(nameof(HealHealth),TimeForRegeneration);
    }

    public override void DestroyHimself()
    {
        const string ppname = "EnemyGreenTutorial";
        if(!Save.console)
            PlayerPrefs.SetInt(ppname,PlayerPrefs.GetInt(ppname,0) + 1);
        Level.EXP += 3;
        Destroy(this);
    }
}
