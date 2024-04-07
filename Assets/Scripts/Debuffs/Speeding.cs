public class Speeding : Debuff
{
    public override string animationName => "IsRunning";
    public override float time => Time;
    float Time, Multipler;
    MoveBase moveBase;
    public Speeding(float time,float multipler)
    {
        Time = time;
        Multipler = multipler;
    }
    public override void OnAdd(DebuffBank bank)
    {
        base.OnAdd(bank);
        moveBase = bank.GetComponent<MoveBase>();
        moveBase.SetSpeedMultiplierTemporary(Multipler,Time);
    }
}
