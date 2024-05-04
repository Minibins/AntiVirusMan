using System;
using UnityEngine;

public class EggsStats : MonoBehaviour
{
    public EggItem[] eggItems;

    private void Start()
    {
        eggItems = GetComponentsInChildren<EggItem>();
    }

    public void UpdateStats()
    {
        foreach (EggItem eggItem in eggItems)
        {
            eggItem.UpdateStats();
        }
    }
}