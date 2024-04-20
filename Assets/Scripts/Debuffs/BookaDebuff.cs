public class BookaDebuff : Debuff
{
    public override string animationName => "IsBold";

    public override void OnAdd(DebuffBank bank)
    {
        bank.GetComponent<Enemy>().AddBookaComponent();
        base.OnAdd(bank);
    }
}