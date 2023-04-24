using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;

    void Update()
    {
        transform.position = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z);
    }
}
