using UnityEngine;

public class JumpOnEnemyUpgrade : Upgrade
{
    protected override void OnTake()
    {
        base.OnTake();

        foreach(Enemy enemy in GameObject.FindObjectsOfType<Enemy>())
        {
            enemy.AddBookaComponent();
        }
    }
}
