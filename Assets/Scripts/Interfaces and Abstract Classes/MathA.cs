using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathA
{
    public static Vector2 RotatedVector(Vector2 VectorToRotate,float angle)
    {
        float rotationInRadians=angle*Mathf.PI/180;
        return new Vector2(VectorToRotate.x * Mathf.Cos(rotationInRadians) - VectorToRotate.y * Mathf.Sin(rotationInRadians),
                                            VectorToRotate.x * Mathf.Sin(rotationInRadians) + VectorToRotate.y * Mathf.Cos(rotationInRadians)
                                             );
    }
    public static Vector2 RotatedVector(Vector2 VectorToRotate,Vector2 angleExample)
    {
    return angleExample.normalized* VectorToRotate.magnitude;
    }
}
