using UnityEngine;

public class pc : MonoBehaviour
{
    private GameManager GamManager;
    private void Start()
    {
        GamManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy projectile") || other.gameObject.CompareTag("ATACK EVERYBODY"))
        {
            GamManager.TakeDamage(other.gameObject.GetComponent<atackprojectile>().power);
        }
    }
}
