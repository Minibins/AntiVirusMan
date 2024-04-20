using UnityEngine;

public class EnablerUpgrade : Upgrade
{
    [SerializeField] bool isEnabliyng =true;
    [SerializeField] GameObject ToEnable;
    protected override void OnTake()
    {
        base.OnTake();
        ToEnable.SetActive(isEnabliyng);
    }
}
