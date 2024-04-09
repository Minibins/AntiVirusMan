using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class UpgradeButton : MonoBehaviour
{
    public int id;
    public static Dictionary<int, Action> UpgradeActions = new Dictionary<int, Action>();

    public void onclick()
    {
        if(id != -1)
        {
            LevelUP.Items[id].IsTaken = true;
            if(UpgradeActions.ContainsKey(id)) UpgradeActions[id]();
            LevelUP.AddPickedItem(LevelUP.Items[id]);
        }
        LevelUP.Select();

        var buttons = UiElementsList.instance.Panels.levelUpPanel;
        for (int i = buttons.Button1.transform.childCount - 1; i >= 0;)
            Destroy(buttons.Button1.transform.GetChild(i--).gameObject);
        for (int i = buttons.Button2.transform.childCount - 1; i >= 0;)
            Destroy(buttons.Button2.transform.GetChild(i--).gameObject);
        for (int i = buttons.Button3.transform.childCount - 1; i >= 0;)
            Destroy(buttons.Button3.transform.GetChild(i--).gameObject);
    }
}