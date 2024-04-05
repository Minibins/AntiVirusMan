using System;

using UnityEngine;
public class ExpDoor : AbstractConduitionDoor
{
    [SerializeField] int ExpReqired;
    [SerializeField] ConditionMode Mode = ConditionMode.More;
    [SerializeField] bool countUpgrades;
    enum ConditionMode
    {
        More,
        Less,
        Equal
    }
    override protected bool Conduition()
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