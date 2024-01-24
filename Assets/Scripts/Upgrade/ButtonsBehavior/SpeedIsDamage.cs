using UnityEngine;

public class SpeedIsDamageUpgrade : Upgrade
{
    protected override void OnTake()
    {
        base.OnTake();
        GameObject.FindObjectOfType<PlayerAttack>().isSpeedIsDamage=true;
    }
}
