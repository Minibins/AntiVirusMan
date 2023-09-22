using System.Collections;
using UnityEngine;

public class PUSHKA : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private float TimeReload;
    [SerializeField] private GameObject SpawnPoinBullet;
    [SerializeField] private bool IsShoot;
    [SerializeField] private GameObject Coleso;


    private void Update()
    {
        if (gameObject.GetComponent<SpriteRenderer>().enabled)
        {
            Coleso.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void StartShoot()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
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
