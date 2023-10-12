using UnityEngine;
public class AttackProjectile : MonoBehaviour
{
    [SerializeField, Min(0)] private int _damage;
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
        switch (_velosity.x)
        {
            case 0:
                _move.MoveHorizontally(_velosity.x); break;
        }
        switch (_velosity.y)
        {
            case 0:
                _move.MoveVertically(_velosity.y); break;
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
}
