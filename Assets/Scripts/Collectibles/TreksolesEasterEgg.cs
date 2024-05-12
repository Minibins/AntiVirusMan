using UnityEngine;

public class TreksolesEasterEgg : EasterEgg
{
    SpriteRenderer _renderer;
    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        _renderer.color = TreksolesUpgrade.color.a<=color.a ? TreksolesUpgrade.color : color;
        canPick = TreksolesUpgrade.IsTreksoled;
    }
}
