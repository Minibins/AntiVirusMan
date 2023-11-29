using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractPortal : MonoBehaviour
{
    [SerializeField] protected GameObject secondPortal;
    public GameObject SecondPortal { set => secondPortal = value; }
    protected void Teleport(Transform teleporting)
    {
        teleporting.position = secondPortal.transform.position;
    }
}
