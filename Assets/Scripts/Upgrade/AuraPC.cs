using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraPC : MonoBehaviour
{
    [SerializeField] private float Damage;
    [SerializeField] private GameObject sprite;
    private bool isStart;
    public bool IsStartWork;
    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && isStart != true && IsStartWork == true)
        {
            other.GetComponent<Move>()._speed = other.GetComponent<Move>()._speed /= 2;
            StartCoroutine(GiveDamage(other));
            isStart = true;
            sprite.SetActive(true);
        }
    }

    IEnumerator GiveDamage(Collider2D other)
    {
        yield return new WaitForSeconds(1f);
        other.GetComponent<Health>().ApplyDamage(Damage);
        print("test");
    }
}
