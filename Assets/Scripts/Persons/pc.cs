using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PC : MonoBehaviour
{
    [SerializeField] Animator animator;

    
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
}
