using UnityEngine;
[RequireComponent(typeof(Enemy))]
public class DropOnPCReach : MonoBehaviour
{
    [SerializeField] private GameObject drop;
    private void Awake()
    {
        if(drop != null) GetComponent<Enemy>().onComputerReach += () => Instantiate(drop,transform.position,Quaternion.identity);
    }
}