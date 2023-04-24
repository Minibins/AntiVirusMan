using UnityEngine;

public class mathyr : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer != 10)
        {
            explode();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
        {
            explode();
        }
    }
    void explode()
    {
        GameObject xplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        xplosion.GetComponent<atackprojectile>().power = 0.5f;
        Destroy(gameObject);
    }
}


