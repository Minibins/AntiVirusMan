using UnityEngine;
[RequireComponent(typeof(Animator))]
public class Attack : MonoBehaviour
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
    private Animator _animator;
    private Health _healthTarget;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
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
