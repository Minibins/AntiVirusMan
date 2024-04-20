public class AcidDebuff : Debuff    
{
    public override string animationName => "Acid";
    private Health health;
    public override void OnAdd(DebuffBank bank)
    {
        base.OnAdd(bank);
        if(bank.TryGetComponent<Health>(out health))
        {
            health.multiplerDamage.summingMultiplers.Add(2);
        }
    }
    public override void Clear()
    {
        base.Clear();
        if(health != null )
        {
            health.multiplerDamage.summingMultiplers.Remove(2);
        }
    }
}
