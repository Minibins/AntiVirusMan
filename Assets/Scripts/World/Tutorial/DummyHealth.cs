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
        Destroy(this);
    }
}
