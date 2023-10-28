using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExpCollectible : MonoBehaviour
{
    [SerializeField] private float Exp;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Level.TakeEXP(Exp);
            Destroy(gameObject);
        }
    }
}
