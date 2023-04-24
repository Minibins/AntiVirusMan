using System.Collections;
using UnityEngine;

public class PUSHKA : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private float TimeReload;
    [SerializeField] private GameObject SpawnPoinBullet;
    [SerializeField] private bool IsShoot;

    public void StartShoot()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        while (IsShoot)
        {
            yield return new WaitForSeconds(TimeReload);
            GameObject bullet = Instantiate(Bullet, SpawnPoinBullet.transform.position, transform.rotation);
        }
    }
}
