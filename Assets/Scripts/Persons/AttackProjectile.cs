using UnityEngine;
public class AttackProjectile : MonoBehaviour
{
    [SerializeField, Min(0)] private int _damage;
    [SerializeField] private bool ExplodeIfCollideWithWall;
    [SerializeField] private GameObject Explosion;
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
    private IDamageble _Target;

    private void Awake()
    {
        _move = GetComponent<MoveBase>();
    }
    protected virtual void Start()
    {
        if (_move != null) {
            _move.MoveBoth(_velosity);
        };
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnSomethingEnter2D(collision);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ExplodeIfCollideWithWall)
        {
            Instantiate(Explosion,transform.position,transform.rotation);
            Destroy(gameObject,0f);
        }
        OnSomethingEnter2D(collision.collider);
    }
    private void OnSomethingEnter2D(Collider2D collision)
    {
        
        if(((_mask.value & (1 << collision.gameObject.layer)) != 0) && collision.gameObject.TryGetComponent<IDamageble>(out _Target))
        {
            _Target.OnDamageGet(Damage);
        }
    }
}
