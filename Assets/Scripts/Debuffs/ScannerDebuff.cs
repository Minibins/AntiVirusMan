using System.Collections.Generic;
using System.Linq;

using Unity.VisualScripting;

public class ScannerDebuff : Debuff
{
    public static List<DebuffBank> owners = new();
    public override string animationName { get => "IsScan"; }
    public override void OnAdd(DebuffBank bank)
    {
        base.OnAdd(bank);
        owners.Add(bank);
        owners = owners.Where(o => !o.IsDestroyed()).ToList();
    }
}