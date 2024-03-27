using UnityEngine;

public class Scanner : AbstractAttack
{
    protected override GameObject attack()
    {
        GameObject laserBeam =base.attack();
        laserBeam.transform.SetParent(transform);
        Destroy(laserBeam, 1);
        laserBeam.GetComponent<AttackProjectile>()._velosity=Vector2.right*transform.lossyScale.x;
        return laserBeam;
    }
}
