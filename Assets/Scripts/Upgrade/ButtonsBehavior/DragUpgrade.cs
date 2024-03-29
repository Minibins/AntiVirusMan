using Unity.VisualScripting;

using UnityEngine;

public class DragUpgrade : Upgrade
{
    protected override void OnTake()
    {
        base.OnTake();
        foreach(TurretLikeUpgrade turret in GameObject.FindObjectsOfType<TurretLikeUpgrade>())
            turret.AddComponent<DRAG>();
        foreach(DraggableAsTurret draggable in GameObject.FindObjectsOfType<DraggableAsTurret>()) 
            draggable.AddComponent<DRAG>();
        GameObject.FindObjectOfType<PChealth>().AddComponent<DRAG>();
        FindObjectOfType<InstantiateWall>().canmove = true;
    }
}
