using UnityEngine;
[RequireComponent(typeof(Move))]
public class AtackProjectile : MonoBehaviour
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
        if (_velosity.x != 0f)
        {
            _move.MoveHorizontally(_velosity.x);
        }
        if (_velosity.y != 0f)
        {
            _move.MoveVertically(_velosity.y);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((_mask.value & (1 << collision.gameObject.layer)) != 0) && collision.gameObject.TryGetComponent<Health>(out _healthTarget))
        {
            _healthTarget.ApplyDamage(_damage);
        }
    }
    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
