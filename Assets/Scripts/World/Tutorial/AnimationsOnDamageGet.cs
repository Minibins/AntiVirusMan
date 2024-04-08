using UnityEngine;

public class AnimationsOnDamageGet : MonoBehaviour, IDamageble
{
    [SerializeField] Animator animator;
    [SerializeField] string DamageTriggerName;
    public void OnDamageGet(float Damage,IDamageble.DamageType type)
    {
        animator.SetTrigger(DamageTriggerName);
    }
}
