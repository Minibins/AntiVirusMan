using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
public class AbstractOnDeathAction : MonoBehaviour
{
    [SerializeField] private UnityEvent Action;

    private void Awake() => GetComponent<Health>().OnDeath += Action.Invoke;
}

[RequireComponent(typeof(Health))]  
public class AbstractOnDamageAction : MonoBehaviour
{
    [SerializeField] private UnityEvent Action;

    private void Awake() => GetComponent<Health>().OnApplyDamage += Action.Invoke;
}