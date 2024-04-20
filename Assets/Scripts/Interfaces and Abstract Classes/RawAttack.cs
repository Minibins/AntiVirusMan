using UnityEngine;

public class RawAttack : MonoBehaviour
{
    [SerializeField] public Health target;
    [SerializeField] private int damage;

    public void Damage()
    {
        target.ApplyDamage(damage);
        Destroy(gameObject);
    }
}