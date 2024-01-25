using UnityEngine;
using System.Collections.Generic;
using DustyStudios.MathAVM;
using UnityEngine.Tilemaps;
using System;
public class TreksolesUpgrade : Upgrade
{
    [SerializeField] Tilemap[] renderers;
    List<GameObject> children = new List<GameObject>();

    private void Awake()
    {
        for(int i = transform.childCount; 0 < i;)
        {
                children.Add(transform.GetChild(--i).gameObject);
        }
    }
    private void FixedUpdate()
    {
        Color color = new Color(1f,1f,1f,Math.Min(Math.Max(renderers[0].color.a - 0.1f*MathA.OneOrNegativeOne(IsTaken&&Player.IsJump),0),1));
        foreach(Tilemap renderer in renderers)
        {
            renderer.color = color;
        }
        foreach(var c in children)
        {
            c.SetActive(color.a != 0f);
        }
    }
}
