using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
public class AbstractOnDeathAction : MonoBehaviour
{
    [SerializeField] private UnityEvent Action;

    private void Awake()
    {
        GetComponent<Health>().OnDeath += Action.Invoke;
    }
}