using UnityEngine;

public class TrainMove : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }
}