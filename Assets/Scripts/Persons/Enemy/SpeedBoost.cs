using System.Linq;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] private LayerMask _maskWhoBoosts;
    [SerializeField] private float _multiplierSpeed;
    [SerializeField] private float _durationBoost;
    [SerializeField] private float _ReloadTime;
    private MoveBase _moveTarget;
    private Animator _animator;
    private bool _reloadNow = false;
    private Collider2D[] _targets;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        Setboost();
    }
    private void Setboost()
    {
        _reloadNow = false;
        _targets = Physics2D.OverlapCircleAll(transform.position, 0.2f, _maskWhoBoosts);
        _moveTarget = _targets.Where(x => x.gameObject != gameObject).
                 Select(x => x.gameObject.GetComponent<MoveBase>()).
                Where(x => x != null && !x.IsMultiplierBoost()).
                FirstOrDefault();
        if (_moveTarget != null)
        {
            _moveTarget.SetSpeedMultiplierTemporary(_multiplierSpeed, _durationBoost);
            _animator.SetTrigger("peenok");
            _reloadNow = true;
        }
        Invoke(nameof(Setboost), _reloadNow ? _ReloadTime : 0.2f);
        
    }
}

