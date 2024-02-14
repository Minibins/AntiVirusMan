using System.Collections;

using UnityEngine;
using UnityEngine.Events;

public class RandomTimedAction : MonoBehaviour
{
    [SerializeField] Vector2 timeRange;
    [SerializeField] UnityEvent Action;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(Random.Range(timeRange.x,timeRange.y));
        Action.Invoke();
    }
}
