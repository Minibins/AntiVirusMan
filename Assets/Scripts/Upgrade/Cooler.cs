using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooler : TurretLikeUpgrade
{
    PointEffector2D effector;
    void Awake()
    {
        effector = GetComponent<PointEffector2D>();
    }
    public override void OnDrag()
    {
        effector.forceMagnitude *= 2; 
    }
    public override void OnDragEnd()
    {
        effector.forceMagnitude /= 2;
    }
}
