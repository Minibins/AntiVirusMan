public class Debuff
{
    protected DebuffBank bank;
    public virtual float time { get => 9999; }
    public virtual string animationName { get => ""; }
    public virtual void OnAdd(DebuffBank bank)
    {
        this.bank = bank;
    }
    public virtual void Clear() 
    { 
        
    }
}