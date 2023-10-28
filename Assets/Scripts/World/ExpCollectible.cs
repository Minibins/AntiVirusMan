
using UnityEngine;

public class ExpCollectible : MonoBehaviour
{
    [SerializeField] private float Exp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Level.TakeEXP(Exp);
            Destroy(gameObject);
        }
    } 
}
