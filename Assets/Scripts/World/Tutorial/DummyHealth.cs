using System.Collections;

using UnityEngine;

public class DummyHealth : Health
{
    [SerializeField] private const int TimeForRegeneration = 1;
    protected override void Awake()
    {
        base.Awake();
        OnApplyDamage += SetNextFinishing;
    }

    public override void ApplyDamage(float damage)
    {
        base.ApplyDamage(1);
        Invoke(nameof(HealHealth),TimeForRegeneration);
    }

    private void HealHealth()
    {
        base.HealHealth(1);
        animator.SetTrigger("Heal");
    }

    private void SetNextFinishing()
    {
        animator.SetTrigger("Finishing");
    }

    public override void DestroyHimself()
    {
        const string ppname = "EnemyGreenTutorial";
        PlayerPrefs.SetInt(ppname,PlayerPrefs.GetInt(ppname,0) + 1);
        Level.EXP += 3;
        Destroy(this);
    }
}
