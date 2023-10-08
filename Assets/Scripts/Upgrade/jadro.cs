using UnityEngine;

public class jadro : MonoBehaviour
{

    [SerializeField] private Vector3 dir;
    [SerializeField] private float speed;
    private void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.rotation * dir * speed  /*gameObject.GetComponent<Rigidbody2D>().angularVelocity*/);
    }
}
