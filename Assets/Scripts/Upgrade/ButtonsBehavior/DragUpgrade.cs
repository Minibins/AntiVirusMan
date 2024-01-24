using Unity.VisualScripting;

using UnityEngine;

public class DragUpgrade : Upgrade
{
    protected override void OnTake()
    {
        base.OnTake();
        GameObject.FindObjectOfType<LaserGun>().AddComponent<DRAG>();
        GameObject.FindObjectOfType<PChealth>().AddComponent<DRAG>();
        GameObject.FindObjectOfType<PUSHKA>().AddComponent<DRAG>();
        FindObjectOfType<InstantiateWall>().canmove = true;
    }
}
