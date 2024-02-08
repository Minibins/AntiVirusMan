using UnityEngine;
[RequireComponent(typeof(Health))]
public class AbstractOnDeathAction : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Health>().OnDeath+=Action;
    }
    virtual protected void Action()
    {

    }
}
