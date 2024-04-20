using UnityEngine;

public class PlayerRotatable : MonoBehaviour, IDamageble
{
    public void OnDamageGet(float damage, IDamageble.DamageType type)
    {
        transform.localScale.Set(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}