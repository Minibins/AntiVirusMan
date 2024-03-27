using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackChanger : Upgrade
{
    [SerializeField] AbstractAttack attack;
    protected override void OnTake()
    {
        base.OnTake();
        GameObject.FindObjectOfType<PlayerAttack>().MainAttack = attack;
    }
}
