using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AbstractEnemy))]
public class AbstractOnEnemyReachAction : MonoBehaviour
{
    [SerializeField] private UnityEvent Action;
    private void Awake()
    {
        GetComponent<AbstractEnemy>().onComputerReach += Action.Invoke;
        Destroy(this);
    }
}