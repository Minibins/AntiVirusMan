using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReferenceItem : MonoBehaviour
{
    public void OnMouseDown()
    {
        if(EasterEggsForDummies.isLookingForReferences)
        {
            EasterEggsForDummies.Glass.transform.position = transform.position;
            EasterEggsForDummies.isLookingForReferences = false;
        }
    }
}
