using System.Collections;
using UnityEngine;

public class PUSHKA : TurretLikeUpgrade, IDamageble
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] public float TimeReload;
    [SerializeField] private GameObject SpawnPoinBullet;
    private SpriteRenderer _sprite;
    [SerializeField] private Sprite uncharged, charged;
    private bool istemporaryboost;
    public bool IsTemporaryBoost 
    { 
        get => istemporaryboost;
        set
        {
            _sprite.sprite = value ? charged : uncharged;
            istemporaryboost = value;
        }
    }
    public void OnDamageGet(float Damage,IDamageble.DamageType type)
    {
        transform.rotation = new Quaternion(0,0,transform.rotation.z * -1,transform.rotation.w);
        IsTemporaryBoost = true;
    }
    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    protected override void OnTake()
    {
        base.OnTake();
        IsTemporaryBoost = false;
        StartCoroutine(Shoot());
    }

    public IEnumerator Shoot()
    {
        yield return new WaitForSeconds(TimeReload);
        while (IsTaken)
        {
            GameObject bullet = Instantiate(Bullet, SpawnPoinBullet.transform.position, transform.rotation);
            if(IsTemporaryBoost)
            {
                bullet.GetComponent<AttackProjectile>().Damage *= 2; 
                IsTemporaryBoost = false;
            }
            yield return new PrecitionWait(TimeReload, TimeReload);
        }
    }
    override public void OnDrag()
    {
        StopAllCoroutines();
        StartCoroutine(Shoot());
        IsTemporaryBoost = true;
        TimeReload = 0.2f;
    }
    override public void OnDragEnd()
    {
        TimeReload = 3f;
    }
}
