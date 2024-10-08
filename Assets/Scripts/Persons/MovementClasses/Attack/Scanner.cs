using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scanner : AbstractAttack
{
    private GameObject Laser;

    protected override GameObject attack()
    {
        GameObject laserBeam = base.attack();
        laserBeam.transform.SetParent(transform);
        Destroy(laserBeam, 1);
        AttackProjectile projectile = laserBeam.GetComponent<AttackProjectile>();
        projectile._velosity = Vector2.right * transform.lossyScale.x;
        projectile.Damage = playerAttack.Damage;
        Laser = laserBeam;
        return laserBeam;
    }

    public override void StopAttack()
    {
        if (Laser != null) Destroy(Laser);
        foreach (IScannable target in GetAllTargets()) target.StopScan();
    }

    public void EndAttack()
    {
        if (Laser != null) Destroy(Laser);
        EndScan();
    }

    private static List<IScannable> GetAllTargets()
    {
        List<IScannable> results = new List<IScannable>();
        foreach (DebuffBank debuffed in ScannerDebuff.owners)
            if (!debuffed.IsDestroyed())
                foreach (IScannable target in debuffed.GetComponents<IScannable>())
                    results.Add(target);
        ScannerDebuff.owners.Clear();
        return results;
    }

    public static void EndScan()
    {
        foreach (IScannable target in GetAllTargets()) target.EndScan();
    }
}