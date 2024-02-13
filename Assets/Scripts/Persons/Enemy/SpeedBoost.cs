using System.Collections;
using System.Linq;
using UnityEngine;

public class SpeedBoost : TagCollisionChecker
{
    [SerializeField] private float _multiplierSpeed;
    [SerializeField] private float _durationBoost;
    [SerializeField] private float _ReloadTime;
    private Animator _animator;
    private bool _reloadNow = false;
    private void Start()
    {
        _animator = GetComponentInParent<Animator>();
        StayAction+=()=>StartCoroutine(Setboost());
    }
    protected override bool StayCondition(Collider2D other)=> !_reloadNow && base.StayCondition(other);
    private IEnumerator Setboost()
    {
        if(EnteredThings.Count> 0)
        {
            MoveBase[] moveTargets = EnteredThings.Where(x => x.gameObject != gameObject).
                     Select(x => x.gameObject.GetComponent<MoveBase>()).
                    Where(x => x != null && !x.IsMultiplierBoost()).ToArray();
            if(moveTargets != null && moveTargets.Length > 0)
            {
                _reloadNow = true;
                for(int i = moveTargets.Length; --i>=0;)
                {
                    if(moveTargets[i] != null)
                    {
                        _animator.SetTrigger("peenok");
                        moveTargets[i].SetSpeedMultiplierTemporary(_multiplierSpeed,_durationBoost);
                        yield return new WaitForSeconds(_ReloadTime);
                    }
                }
            }
        }
        _reloadNow = false;
    }
}

