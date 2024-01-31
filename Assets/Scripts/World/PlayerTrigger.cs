using UnityEngine;
using UnityEngine.Events;

public class PlayerTrigger : PlayersCollisionChecker
{
    [SerializeField] UnityEvent enterEvent;
    private void Awake()
    {
        EnterAction += ()=> enterEvent.Invoke();
    }
}
