using UnityEngine;
public class SewerHatch : MonoBehaviour
{
    [SerializeField] GameObject egg;
    public void Open()
    {
        GetComponent<Animator>().SetTrigger("Open");
        Instantiate(egg,transform.position,Quaternion.identity);
        Destroy(this);
    }
}
