﻿using System.Collections;
using UnityEngine;

public class LaserGun : MonoBehaviour,Draggable
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private float TimeReload;
    [SerializeField] private Transform SpawnPoinBullet;
    [SerializeField] private bool IsShoot;
    new Transform transform;
    private void Awake()
    {
        transform = base.transform;
    }
    public void StartShoot()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        StartCoroutine(Shoot());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("AntivirusAtack") || other.CompareTag("ATACK EVERYBODY"))
        {
            if (transform.rotation.y == 0)
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }
            else
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("AntivirusAtack") || other.gameObject.CompareTag("ATACK EVERYBODY"))
        {
            transform.rotation = new Quaternion(0, transform.rotation.y + 180, 0, 0);
        }
    }
    IEnumerator Shoot()
    {
        while (IsShoot)
        {
            yield return new WaitForSeconds(TimeReload);
            AttackProjectile bullet = Instantiate(Bullet, SpawnPoinBullet.position, transform.rotation).GetComponent<AttackProjectile>();
            bullet._velosity=MathAVM.MathA.RotatedVector(bullet._velosity,transform.rotation.eulerAngles.z);
        }
    }
    public void OnDrag()
    {
        StopAllCoroutines();
        TimeReload /= 3;
        StartCoroutine (Shoot());
    }
    public void OnDragEnd()
    {
        StopAllCoroutines();
        TimeReload *= 3;
        StartCoroutine(Shoot());
    }
}
