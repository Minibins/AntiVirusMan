using UnityEngine;

public class Collectible : MonoBehaviour
{
    public bool canPick = true;
    public virtual void Pick(GameObject picker)
    {
        GetComponent<Animator>().SetTrigger("Pick");
    }
}