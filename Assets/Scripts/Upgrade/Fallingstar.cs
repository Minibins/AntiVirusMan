using UnityEngine;

public class Fallingstar : MonoBehaviour
{
    public Vector3 fallvector;
    public float[] rangefortp;
    private int timeforlive = 100;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("LaserGun"))
        {
            other.transform.position = new Vector3(Random.Range(rangefortp[0], rangefortp[1]), other.transform.position.y, other.transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        transform.position += fallvector;
        timeforlive--;
        if (timeforlive == 0)
        {
            Destroy(gameObject);
        }
    }
}
