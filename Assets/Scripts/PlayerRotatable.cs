using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotatable : MonoBehaviour,IDamageble
{
    public void OnDamageGet(int damage)
    {
        transform.localScale.Set(transform.localScale.x*-1,transform.localScale.y,transform.localScale.z);
    }

}
