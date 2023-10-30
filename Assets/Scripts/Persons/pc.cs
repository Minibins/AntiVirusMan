using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PC : MonoBehaviour
{
    [SerializeField] private int radius;
    private Health health;
    private Animator animator;
    private Transform rozetka;
    private Animator rozetkaAnim;
    private bool lowchrge;
    private void Start()
    {
        health = GetComponentInParent<Health>();
        animator = GetComponentInParent<Animator>();
        rozetka = GameObject.Find("Rozetka").transform;
        rozetkaAnim=rozetka.GetComponent<Animator>();
        StartCoroutine(LowCharge());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Enemy")
        {
            animator.SetTrigger("NoCalm");
        }
        
    }
    public void EnemyKilled()
    {
            animator.SetTrigger("HeKilledEnemy");    
    }
    private void FixedUpdate()
    {
        if(Vector2.Distance(rozetka.position,transform.position) > radius)
        {
            rozetkaAnim.SetBool("Sad",true);
            lowchrge = true;
        }
        else
        {
            rozetkaAnim.SetBool("Sad",false);
            lowchrge = false;
        }
    }
    IEnumerator LowCharge()
    {
        while(true)
        {
        yield return new WaitForSeconds(5);
        while(lowchrge) {
            health.ApplyDamage(1);
            yield return new WaitForSeconds(5);
        }
        }
        
        
    }
}
