using UnityEngine;

public class EnablerUpgrade : Upgrade
{
    [SerializeField] GameObject ToEnable;
    protected override void OnTake()
    {
        base.OnTake();
        ToEnable.SetActive(true);
    }
}
