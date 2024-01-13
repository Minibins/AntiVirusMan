using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractPortal : MonoBehaviour
{
    [SerializeField] public GameObject secondPortal;
    public void Teleport(Transform teleporting)
    {
        teleporting.position = secondPortal.transform.position;
    }
}
