using UnityEngine;
public class SewerHatch : MonoBehaviour
{
    [SerializeField] GameObject egg, drop;
    public void Open()
    {
        GetComponent<Animator>().SetTrigger("Open");
        Instantiate(EasterEgg.isEaster ? egg : drop,transform.position,Quaternion.identity);
        if(EasterEgg.isEaster) Destroy(this);
    }
}
