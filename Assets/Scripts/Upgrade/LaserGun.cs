using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class LaserGun : TurretLikeUpgrade, IDamageble
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private float TimeReload;
    [SerializeField] private Transform SpawnPoinBullet;
    new Transform transform;
    int greases;
    private void Awake()
    {
        transform = base.transform;
    }
    public void OnDamageGet(int Damage,IDamageble.DamageType type)
    {
        greases += Damage;
    }

    protected override void OnTake()
    {
        base.OnTake();
        ResetShoot();
    }
    IEnumerator Shoot()
    {
        yield return new PrecitionWait(TimeReload,1);
        while (IsTaken)
        {
            AttackProjectile bullet = Instantiate(Bullet, SpawnPoinBullet.position, transform.rotation).GetComponent<AttackProjectile>();
            bullet._velosity=DustyStudios.MathAVM.MathA.RotatedVector(bullet._velosity,transform.rotation.eulerAngles.z);
            Level.EXP+=Math.Min((greases+0.5f)/2,1);
            greases = -2;
            yield return new PrecitionWait(TimeReload,1);
        }
    }
    public override void OnDrag()
    {
        TimeReload /= 3;
        ResetShoot();
    }
    public override void OnDragEnd()
    {
        TimeReload *= 3;
        ResetShoot();
    }
    void ResetShoot()
    {
        StopAllCoroutines();
        StartCoroutine(Shoot());
    }
}
