using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpDoor : AbstractConduitionDoor
{
    [SerializeField] int ExpReqired;
    override protected bool Conduition()
    {
        return Level.EXP < ExpReqired;
    }
}