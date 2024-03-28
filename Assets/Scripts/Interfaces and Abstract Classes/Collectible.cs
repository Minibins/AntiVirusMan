using UnityEngine;

public class Collectible : MonoBehaviour
{
    public virtual void Pick(GameObject picker)
    {
        GetComponent<Animator>().SetTrigger("Pick");
    }
}