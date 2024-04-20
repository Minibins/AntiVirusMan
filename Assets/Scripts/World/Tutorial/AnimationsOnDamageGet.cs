using UnityEngine;

public class AnimationsOnDamageGet : MonoBehaviour, IDamageble
{
    [SerializeField] private Animator animator;
    [SerializeField] private string DamageTriggerName;

    public void OnDamageGet(float Damage, IDamageble.DamageType type)
    {
        animator.SetTrigger(DamageTriggerName);
    }
}