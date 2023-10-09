using System.Diagnostics.Eventing.Reader;
using System.Linq;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] private LayerMask _maskWhoBoosts;
    [SerializeField] private float _multiplierSpeed;
    [SerializeField] private float _durationBoost;
    [SerializeField] private float _ReloadTime;
    [SerializeField] private Animator Peenok;
    [SerializeField] private Rigidbody2D Me;
    private Move _moveTarget;
    private bool _reloadNow = false;
    private Collider2D[] _targets;
    private void Start()
    {
        Me = GetComponent<Rigidbody2D>();
        Setboost();
        Peenok = GetComponent<Animator>();
    }
    private void BoostReload()
    {
        _ReloadTime = 1000f;
       Peenok.Play("peenok");
        
        Invoke(nameof(BoostReload), _reloadNow ? _ReloadTime : 1f);
    }
    public void Setboost()
    {

        _targets = Physics2D.OverlapCircleAll(transform.position, 0.2f, _maskWhoBoosts);
        _moveTarget = _targets.Where(x => x.gameObject != gameObject).
                 Select(x => x.gameObject.GetComponent<Move>()).
                Where(x => x != null && !x.IsMultiplierBoost()).
                FirstOrDefault();
        if (_moveTarget != null)
        {
            _moveTarget.SetSpeedMultiplierTemporary(_multiplierSpeed, _durationBoost);
            _reloadNow = true;
            _ReloadTime = 1f;
        }
    }
    public void Stun()
    {
        _reloadNow = false;
    }
    private void Update() {
    if (_reloadNow) { Me.bodyType = RigidbodyType2D.Static; }else { Me.bodyType = RigidbodyType2D.Dynamic; }
    } 
    
}

