using UnityEngine;

public class DragEnemyUpgrade : Upgrade
{
    protected override void OnTake()
    {
        base.OnTake();

        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.AddComponent<DRAG>();
        }
    }
}
