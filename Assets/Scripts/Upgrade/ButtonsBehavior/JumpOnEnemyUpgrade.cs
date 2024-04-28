using UnityEngine;

public class JumpOnEnemyUpgrade : Upgrade
{
    protected override void OnTake()
    {
        base.OnTake();
        foreach(AbstractEnemy enemy in GameObject.FindObjectsOfType<AbstractEnemy>())
        {
            enemy.AddBookaComponent();
        }
    }
}
