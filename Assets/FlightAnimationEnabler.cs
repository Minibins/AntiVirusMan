using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightAnimationEnabler : Upgrade
{
    protected override void OnTake()
    {
        GetComponent<Animator>().SetBool("CanFly",true);
    }
}
