using System;

using UnityEngine;
public class ExpDoor : AbstractConduitionDoor
{
    [SerializeField] private int ExpReqired;
    [SerializeField] private ConditionMode Mode = ConditionMode.More;
    [SerializeField] private bool countUpgrades;
    private enum ConditionMode
    {
        More,
        Less,
        Equal
    }
    protected override bool Conduition()
    {
        float EXP = Level.EXP + Convert.ToInt16(countUpgrades) * Level.EnemyNeedToUpLVL * LevelUP.pickedItems.Count;
        switch(Mode)
        {
            case ConditionMode.Less:
                return EXP < ExpReqired;
            case ConditionMode.Equal:
                return EXP == ExpReqired;
            case ConditionMode.More: 
                return EXP >= ExpReqired;
        }
        return Level.EXP > ExpReqired;
    }
}