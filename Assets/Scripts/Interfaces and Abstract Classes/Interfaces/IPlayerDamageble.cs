public interface IDamageble
{
    public enum DamageType
    {
        Default, Explosion, Everynovment, Punishment
    }
    public void OnDamageGet(int Damage, DamageType type);
}
