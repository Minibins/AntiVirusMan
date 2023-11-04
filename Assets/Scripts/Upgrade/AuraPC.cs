using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraPC : MonoBehaviour
{
    [SerializeField] private float Damage;
    private bool isStart;
    public bool IsStartWork;
    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && isStart != true && IsStartWork == true)
        {
            other.GetComponent<Move>()._speed = other.GetComponent<Move>()._speed /= 2;
            StartCoroutine(GiveDamage(other));
            isStart = true;
            GetComponent<SpriteRenderer>().enabled=true;
        }
    }

    IEnumerator GiveDamage(Collider2D other)
    {
        
        other.GetComponent<Health>().ApplyDamage(Damage);
        yield return new WaitForSeconds(1f);
        isStart = false;
    }
}
