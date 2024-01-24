using System.Collections.Generic;
using UnityEngine;
using System;
public class UpgradeButton : MonoBehaviour
{
    public int id;
    public static Dictionary<int, Action> UpgradeActions = new Dictionary<int, Action>();
    public void onclick()
    {
        LevelUP.Items[id].IsTaken = true;
        if(UpgradeActions.ContainsKey(id)) UpgradeActions[id]();
        Level.IsSelected = true;
    }
}