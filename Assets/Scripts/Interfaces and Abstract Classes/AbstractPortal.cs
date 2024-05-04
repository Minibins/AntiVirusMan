using UnityEngine;

public class AbstractPortal : MonoBehaviour
{
    [SerializeField] public GameObject secondPortal;

    public void Teleport(Transform teleporting)
    {
        teleporting.position = new(secondPortal.transform.position.x,secondPortal.transform.position.y,teleporting.position.z);
    }
}