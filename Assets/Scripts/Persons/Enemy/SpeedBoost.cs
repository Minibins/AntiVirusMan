using System.Collections;
using System.Linq;
using UnityEngine;

public class SpeedBoost : AbstractAura
{
    [SerializeField] private float _multiplierSpeed;
    [SerializeField] private float _durationBoost;
    private Animator _animator;
    private void Awake()
    {
            _animator = GetComponent<Animator>();
        if(_animator == null )
        {
        _animator = GetComponentInParent<Animator>();
        }
    }
    protected override bool StayCondition(Collider2D other)
    {
        if(base.StayCondition(other))
        {
            DebuffBank otherMove = other.GetComponent<DebuffBank>();
            if(otherMove != null)
            {
                return !otherMove.HasDebuffOfType(typeof(Speeding))&&other.GetComponent<MoveBase>()!=null;
            }
        }
        return false;
    }
    protected override IEnumerator AuraAction()
    {
        if(EnteredThings.Count > 0)
        {
            DebuffBank[] moveTargets = EnteredThings.Where(x => x.gameObject != gameObject).
                    Select(x => x.GetComponent<DebuffBank>()).
                    Where(x => x != null && x.GetComponent<MoveBase>()!=null && !x.HasDebuffOfType(typeof(Speeding))).ToArray();
            if(moveTargets != null && moveTargets.Length > 0)
            {
                for(int i = moveTargets.Length; --i >= 0;)
                {
                    if(moveTargets[i] != null)
                    {
                        _animator.SetTrigger("peenok");
                        moveTargets[i].AddDebuff(new Speeding(_durationBoost,_multiplierSpeed));
                        yield return new WaitForSeconds(_ReloadTime);
                    }
                }
            }
        }
    }
}