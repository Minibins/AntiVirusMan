using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionPlatforma : MonoBehaviour
{
    public Waies MyPath;
    public float speed = 1;
    public float maxDistance = .1f;
    private IEnumerator<Transform> pointInPath;
    void Start()
    {
        pointInPath = MyPath.GetNextPathPoint();
        pointInPath.MoveNext();
        transform.position = pointInPath.Current.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (pointInPath == null || pointInPath.Current == null)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * speed);

        var distanceSqure = (transform.position - pointInPath.Current.position).sqrMagnitude;
        if (distanceSqure < maxDistance * maxDistance)
        {
            pointInPath.MoveNext();
        }
    }
}
