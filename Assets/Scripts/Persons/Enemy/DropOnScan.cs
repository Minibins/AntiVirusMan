using UnityEngine;

public class DropOnScan : MonoBehaviour, IScannable
{
    [SerializeField] GameObject drop;
    public void EndScan()
    {
        Instantiate(drop,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
    public void StartScan(){}
    public void StopScan(){}
}
