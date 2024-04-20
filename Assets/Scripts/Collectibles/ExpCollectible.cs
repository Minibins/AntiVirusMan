using UnityEngine;

public class ExpCollectible : Collectible
{
    public float Exp;

    public override void Pick(GameObject picker)
    {
        base.Pick(picker);
        Level.EXP += Exp;
    }
}