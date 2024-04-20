using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Enemy))]
public class AbstractOnEnemyReachAction : MonoBehaviour
{
    [SerializeField] private UnityEvent Action;

    private void Awake()
    {
        GetComponent<Enemy>().onComputerReach += Action.Invoke;
    }
}