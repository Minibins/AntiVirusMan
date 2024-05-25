using UnityEngine;
public class EasterObject : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(EasterEgg.isEaster) ;
    }
}
