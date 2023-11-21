using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawAttack : MonoBehaviour
{
    [SerializeField] public Health target;
    [SerializeField] int damage;
    public void Damage()
    {
            target.ApplyDamage(damage);
            Destroy(gameObject);
    }
}
