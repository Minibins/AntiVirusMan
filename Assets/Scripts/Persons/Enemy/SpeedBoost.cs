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
        _animator = GetComponentInParent<Animator>();
    }
    protected override IEnumerator AuraAction()
    {
        if(EnteredThings.Count > 0)
        {
            MoveBase[] moveTargets = EnteredThings.Where(x => x.gameObject != gameObject).
                     Select(x => x.gameObject.GetComponent<MoveBase>()).
                    Where(x => x != null && !x.IsMultiplierBoost()).ToArray();
            if(moveTargets != null && moveTargets.Length > 0)
            {
                for(int i = moveTargets.Length; --i >= 0;)
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
    }
}