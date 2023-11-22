using UnityEditor;
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
    private Move _move;
    private IDamageble _Target;

    private void Awake()
    {
        _move = GetComponent<Move>();
    }
    protected virtual void Start()
    {
        if (_move != null) {
        if(_velosity.x!=0)
        { _move.MoveHorizontally(_velosity.x);
        }
        if(_velosity.y!=0)
        {
            
                _move.MoveVertically(_velosity.y);
        }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((_mask.value & (1 << collision.gameObject.layer)) != 0) && collision.gameObject.TryGetComponent<IDamageble>(out _Target))
        {
            _Target.OnDamageGet(Damage);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ExplodeIfCollideWithWall)
        {
            Instantiate(Explosion,transform.position,transform.rotation);
            Destroy(gameObject,0f);
        }
    }
}
