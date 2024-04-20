using UnityEngine;
using UnityEngine.Events;

public class PlayerTrigger : PlayersCollisionChecker
{
    [SerializeField] private UnityEvent enterEvent;

    private void Awake()
    {
        EnterAction += () => enterEvent.Invoke();
    }
}