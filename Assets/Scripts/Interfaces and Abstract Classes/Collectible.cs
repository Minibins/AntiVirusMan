using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public virtual void Pick(GameObject picker)
    {
        Destroy(gameObject);
    }
}