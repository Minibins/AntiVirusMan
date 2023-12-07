using UnityEngine;

public class TrainMove : MonoBehaviour
{
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.left * TrainOption._speed * Time.deltaTime);
    }
}