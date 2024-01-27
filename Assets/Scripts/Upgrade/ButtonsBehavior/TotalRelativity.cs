using UnityEngine;

public class TotalRelativity : Upgrade
{
    protected override void OnTake()
    {
        base.OnTake();
        transform.localScale += Vector3.right;
    }
}
