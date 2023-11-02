using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PUSHKA : MonoBehaviour
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] public float TimeReload;
    [SerializeField] private GameObject SpawnPoinBullet;
    [SerializeField] private bool IsShoot;
    [SerializeField] private SpriteRenderer Coleso;
    private SpriteRenderer sprite;
    [SerializeField] private Sprite uncharged;
    [SerializeField] private Sprite charged;
    public bool istemporaryboost;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(transform.rotation.ToString());
        if (collision.tag == "AntivirusAtack")
        {
               transform.rotation= new Quaternion(0, 0,transform.rotation.z*-1,transform.rotation.w);
               istemporaryboost=true;
            sprite.sprite = charged;
        }
        
    }
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void StartShoot()
    {
        sprite.enabled = true;
        istemporaryboost = false;
        StartCoroutine(Shoot());
    }

    public IEnumerator Shoot()
    {
        while (IsShoot)
        {
            
            GameObject bullet = Instantiate(Bullet, SpawnPoinBullet.transform.position, transform.rotation);
            if(istemporaryboost)
            {
                bullet.GetComponent<AttackProjectile>().Damage *= 2; 
                istemporaryboost=false;
                sprite.sprite = uncharged;
            }
            yield return new WaitForSeconds(TimeReload);
        }
    }
}
