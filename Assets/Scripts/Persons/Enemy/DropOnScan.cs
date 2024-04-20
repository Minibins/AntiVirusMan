using UnityEngine;

public class DropOnScan : MonoBehaviour, IScannable
{
    [SerializeField] private GameObject drop;

    public void EndScan()
    {
        if (drop != null) Instantiate(drop, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void StartScan()
    {
    }

    public void StopScan()
    {
    }
}