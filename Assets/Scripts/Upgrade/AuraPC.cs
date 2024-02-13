using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraPC : AbstractAura
{
    [SerializeField] private float Damage, SelfExpDamage;
    private bool isStart;
    void OnEnable()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }


    protected override IEnumerator AuraAction()
    {
        EnteredThings[0].GetComponent<MoveBase>()._speed = EnteredThings[0].GetComponent<MoveBase>()._speed /= 2;
        Level.EXP -= SelfExpDamage;
        EnteredThings[0].GetComponent<Health>().ApplyDamage(Damage);
        yield return new WaitForSeconds(_ReloadTime);
        isStart = false;
    }
}
