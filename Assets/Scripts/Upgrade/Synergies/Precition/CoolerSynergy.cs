using UnityEngine;
public class CoolerPrecitionSynergy : Synergy
{
    protected override void onTake()
    {
        gameObject.GetComponentInChildren<PointEffector2D>().forceMagnitude *= 4;
    }
}
