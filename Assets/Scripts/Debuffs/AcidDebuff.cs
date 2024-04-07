public class AcidDebuff : Debuff    
{
    public override string animationName => "Acid";
    Health health;
    public override void OnAdd(DebuffBank bank)
    {
        base.OnAdd(bank);
        if(bank.TryGetComponent<Health>(out health))
        {
            health.multiplerDamage.multiplers.Add(2);
        }
    }
    public override void Clear()
    {
        base.Clear();
        if(health != null )
        {
            health.multiplerDamage.multiplers.Remove(2);
        }
    }
}
