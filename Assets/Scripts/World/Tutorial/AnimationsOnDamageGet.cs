using UnityEngine;

public class AnimationsOnDamageGet : MonoBehaviour, IDamageble
{
    [SerializeField] Animator animator;
    [SerializeField] string DamageTriggerName;
    public void OnDamageGet(int Damage)
    {
        animator.SetTrigger(DamageTriggerName);
    }
}