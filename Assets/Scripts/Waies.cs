using UnityEngine;
using System.Collections.Generic;

public class Waies : MonoBehaviour
{
    [SerializeField] private PathTypes PathType;
    [SerializeField] private int movementDirection = 1;
    [SerializeField] private int MoveingTo = 0;
    [SerializeField] private Transform[] PathElements;
    private enum PathTypes
    {
        linear,
        loop
    }

    private void OnDrawGizmos()
    {
        if (PathElements == null || PathElements.Length < 2)
        {
            return;
        }

        for (var i = 1; i < PathElements.Length; i++)
        {
            Gizmos.DrawLine(PathElements[i - 1].position, PathElements[i].position);
        }

        if (PathType == PathTypes.loop)
        {
            Gizmos.DrawLine(PathElements[0].position, PathElements[PathElements.Length - 1].position);
        }
    }

    public IEnumerator<Transform> GetNextPathPoint()
    {
        if (PathElements == null || PathElements.Length < 1)
        {
            yield break;
        }

        while (true)
        {
            yield return PathElements[MoveingTo];

            if (PathElements.Length == 1)
            {
                continue;
            }

            if (PathType == PathTypes.linear)
            {
                if (MoveingTo <= 0)
                {
                    movementDirection = 1;
                }
                else if (MoveingTo >= PathElements.Length - 1)
                {
                    movementDirection = -1;
                }
            }
            MoveingTo = MoveingTo + movementDirection;

            if (PathType == PathTypes.loop)
            {
                if (MoveingTo >= PathElements.Length)
                {
                    MoveingTo = 0;
                }
                if (MoveingTo < 0)
                {
                    MoveingTo = PathElements.Length - 1;
                }
            }
        }
    }
}
