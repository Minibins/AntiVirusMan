using UnityEngine;
[RequireComponent(typeof(Health))]
public class AboveDeath : MonoBehaviour
{
    [SerializeField] LayerMask _deathFromLayers;
    [SerializeField] float _correctionY = 0.5f;
    private Health _health;
    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (((_deathFromLayers.value & (1 << other.gameObject.layer)) != 0) && other.transform.position.y > transform.position.y + _correctionY)
        {
            _health.ApplyDamage(_health.CurrentHealth);
        }
    }
}