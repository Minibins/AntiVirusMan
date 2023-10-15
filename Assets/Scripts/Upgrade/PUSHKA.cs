using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PUSHKA : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private float TimeReload;
    [SerializeField] private GameObject SpawnPoinBullet;
    [SerializeField] private bool IsShoot;
    [SerializeField] private GameObject Coleso;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "AntivirusAtack")
        {
            
                transform.rotation = new Quaternion(0, 0, 45+ transform.rotation.z, 0);
            

        }
    }

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
