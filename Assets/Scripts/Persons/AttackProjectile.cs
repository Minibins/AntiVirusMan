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
    [SerializeField] private LayerMask _mask;
    [SerializeField] private Vector2 _velosity;
    private Move _move;
    private Health _healthTarget;

    private void Awake()
    {
        _move = GetComponent<Move>();
    }
    private void Start()
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
        if (((_mask.value & (1 << collision.gameObject.layer)) != 0) && collision.gameObject.TryGetComponent<Health>(out _healthTarget))
        {
            _healthTarget.ApplyDamage(_damage);
        }
        
    }
    public void DestroyThis(float time = 0f)
    {
        Destroy(gameObject, time);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ExplodeIfCollideWithWall)
        {
            Instantiate(Explosion,transform.position,Quaternion.identity);
            DestroyThis(0f);
        }
    }
}
