using UnityEngine;

public class AttackProjectile : MonoBehaviour
{
    [SerializeField, Min(0)] private float _damage;
    [SerializeField] private bool ExplodeIfCollideWithWall;
    [SerializeField] private GameObject Explosion;
    [SerializeField] private IDamageble.DamageType DamageType;
    private IAttackProjectileModule[] modules;

    public float Damage
    {
        get { return _damage; }
        set { _damage = value < 0 ? 0 : value; }
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
        if (_move != null)
        {
            _move.MoveBoth(_velosity);
        }

        ;
        modules = GetComponents<IAttackProjectileModule>();
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
            Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(gameObject, 0f);
        }
    }

    private void OnSomethingEnter2D(Collider2D collision)
    {
        IDamageble[] _Targets = collision.GetComponents<IDamageble>();
        if ((_mask.value & (1 << collision.gameObject.layer)) != 0 && _Targets.Length != 0)
        {
            foreach (var _Target in _Targets)
            {
                _Target.OnDamageGet(Damage, DamageType);
                try
                {
                    foreach (IAttackProjectileModule module in modules)
                        module.Attack(_Target, collision);
                }
                catch
                {
                    Awake();
                    Start();
                    foreach (IAttackProjectileModule module in modules)
                        module.Attack(_Target, collision);
                }
            }
        }
    }
}