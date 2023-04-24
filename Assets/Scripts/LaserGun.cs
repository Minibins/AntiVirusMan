using System.Collections;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private float TimeReload;
    [SerializeField] private GameObject SpawnPoinBullet;
    [SerializeField] private bool IsShoot;
    public void StartShoot()
    {
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
            Instantiate(Bullet, SpawnPoinBullet.transform.position, transform.rotation);
        }
    }
}
