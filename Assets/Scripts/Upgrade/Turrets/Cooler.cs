using UnityEngine;

public class Cooler : TurretLikeUpgrade
{
    PointEffector2D effector;
    void Awake()
    {
        effector = GetComponentInChildren<PointEffector2D>();
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