using UnityEngine;
public class AttackProjectile : MonoBehaviour
{
    [SerializeField, Min(0)] private int _damage;
    [SerializeField] private bool ExplodeIfCollideWithWall;
    [SerializeField] private GameObject Explosion;
    [SerializeField] IDamageble.DamageType DamageType;
    IAttackProjectileModule[] modules;
    public int Damage
    {
        get
        {
            return _damage;
        }
        set
        {
            _damage = value < 0 ? 0 : value;
        }
    }
    [SerializeField] public LayerMask _mask;
    [SerializeField] public Vector2 _velosity;
    private MoveBase _move;
    private void Awake()
    {
        _move = GetComponent<MoveBase>();
    }
    protected virtual void Start()
    {
        if (_move != null) {
            _move.MoveBoth(_velosity);
        };
        modules=GetComponents<IAttackProjectileModule>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnSomethingEnter2D(collision);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnSomethingEnter2D(collision.collider);
        if (ExplodeIfCollideWithWall)
        {
            Instantiate(Explosion,transform.position,transform.rotation);
            Destroy(gameObject,0f);
        }
    }
    private void OnSomethingEnter2D(Collider2D collision)
    {
        IDamageble _Target;
        if((_mask.value & (1 << collision.gameObject.layer)) != 0 && collision.gameObject.TryGetComponent<IDamageble>(out _Target))
        {
            _Target.OnDamageGet(Damage,DamageType);
            foreach(IAttackProjectileModule module in modules)
            module.Attack(_Target, collision);
        }
    }
}
