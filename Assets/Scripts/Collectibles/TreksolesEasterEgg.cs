using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreksolesEasterEgg : EasterEgg
{
    SpriteRenderer renderer;
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        renderer.color = TreksolesUpgrade.color.a<=color.a ? TreksolesUpgrade.color : color;
        canPick = TreksolesUpgrade.IsTreksoled;
    }
}
