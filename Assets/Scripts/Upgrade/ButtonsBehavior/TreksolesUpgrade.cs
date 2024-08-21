using UnityEngine;
using UnityEngine.Tilemaps;
using System;
public class TreksolesUpgrade : Upgrade
{
    [SerializeField] Tilemap[] renderers;
    Collider2D[] children;
    Transform player;
    float playersHeight;
    public static Color color = new(1f,1f,1f,0f);
    public static bool IsTreksoled => color.a != 0f;
    private void Awake()
    {
        children ??= GetComponentsInChildren<Collider2D>();
        player ??= GameObject.FindObjectOfType<PlayerAttack>().transform;
        try
        {
            BoxCollider2D box =player.GetComponent<BoxCollider2D>();
            playersHeight = box.size.y/2+box.edgeRadius;
        }
        catch
        {
            print(player);
        }
    }
    private void FixedUpdate()
    {
        if(!IsTaken) return;

        color.a = Math.Clamp(color.a - (Player.IsJump ? -0.1f : 0.1f),0,1);
        
        foreach(Tilemap renderer in renderers)
            renderer.color = color;

        foreach(var child in children)
            child.isTrigger = !(IsTreksoled && child.transform.position.y+child.offset.y < player.position.y-playersHeight);
    }
}
