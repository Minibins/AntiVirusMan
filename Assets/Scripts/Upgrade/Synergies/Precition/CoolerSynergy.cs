using UnityEngine;
public class CoolerPrecitionSynergy : Synergy
{
    protected override void onTake()
    {
        GetComponent<PointEffector2D>().forceMagnitude *= 4;
    }
}
