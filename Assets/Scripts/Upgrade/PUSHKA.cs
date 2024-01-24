using System.Collections;
using UnityEngine;

public class PUSHKA : Upgrade, Draggable
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
        if (collision.tag == "AntivirusAtack")
        {
               transform.rotation= new Quaternion(0, 0,transform.rotation.z*-1,transform.rotation.w);
               istemporaryboost=true;
            sprite.sprite = charged;
        }
        
    }
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    protected override void OnTake()
    {
        base.OnTake();

        Coleso.enabled = true;
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
    public void OnDrag()
    {
        StopAllCoroutines();
        StartCoroutine(Shoot());
        istemporaryboost = true;
        TimeReload = 0.2f;
    }
    public void OnDragEnd()
    {
        TimeReload = 3f;
    }
}
