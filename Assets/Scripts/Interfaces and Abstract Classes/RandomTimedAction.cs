using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RandomTimedAction : MonoBehaviour
{
    [SerializeField] private Vector2 timeRange;
    [SerializeField] private UnityEvent Action;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(Random.Range(timeRange.x, timeRange.y));
        Action.Invoke();
    }
}