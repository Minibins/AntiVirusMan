using UnityEngine;
using System.Collections.Generic;
using DustyStudios.MathAVM;
using UnityEngine.Tilemaps;
using System;
public class TreksolesUpgrade : Upgrade
{
    [SerializeField] Tilemap[] renderers;
    Collider2D[] children;
    Transform player;
    float playersHeight;
    private void Awake()
    {
        player = GameObject.FindObjectOfType<Player>().transform;
        children = GetComponentsInChildren<Collider2D>();
        BoxCollider2D box =player.GetComponent<BoxCollider2D>();
        playersHeight = box.size.y+box.edgeRadius;
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
            c.isTrigger = !(color.a != 0f && c.transform.position.y < player.position.y-playersHeight);
        }
    }
}
