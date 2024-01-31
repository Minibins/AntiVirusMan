using UnityEngine;

public class TurretLikeUpgrade : Upgrade, iDraggable
{
    public virtual void OnDrag()
    {
    }
    public virtual void OnDragEnd()
    {
    }
    protected override void OnTake()
    {
        base.OnTake();
        foreach(Behaviour behaviors in GetComponentsInChildren<Behaviour>())
        {
            behaviors.enabled = true;
        }
        foreach(Renderer renderers in GetComponentsInChildren<Renderer>())
        {
            renderers.enabled = true;
        }
        foreach(Rigidbody2D physics in GetComponentsInChildren<Rigidbody2D>())
        {
            physics.simulated = true;
        }
    }
}
