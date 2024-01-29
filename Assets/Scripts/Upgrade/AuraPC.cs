using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraPC : Upgrade
{
    [SerializeField] private float Damage;
    private bool isStart;
    protected override void OnTake()
    {
        base.OnTake();
        GetComponent<SpriteRenderer>().enabled = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !isStart && IsTaken)
        {
            other.GetComponent<MoveBase>()._speed = other.GetComponent<MoveBase>()._speed /= 2;
            StartCoroutine(GiveDamage(other));
            isStart = true;
            GetComponent<SpriteRenderer>().enabled=true;
        }
    }

    IEnumerator GiveDamage(Collider2D other)
    {
        Level.EXP--;
        other.GetComponent<Health>().ApplyDamage(Damage);
        yield return new WaitForSeconds(1f);
        isStart = false;
    }
}
