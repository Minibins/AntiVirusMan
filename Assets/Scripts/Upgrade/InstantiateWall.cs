using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Windows;

public class InstantiateWall : Upgrade
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Wall Wall, MovableWall;
    [SerializeField] private float TimeToStart;
    public bool canmove;
    private static List<Wall> walls = new();
    protected override void OnTake()
    {
        base.OnTake();
        playerAttack=GetComponent<PlayerAttack>();
    }

    public void OnJump()
    {
        if (IsTaken)
        {
            StartCoroutine(SpawnWall());
        }
    }
    IEnumerator SpawnWall()
    {
        RaycastHit2D hit = Hit(); // Тут рейкаст
        Vector2 playerpos=new Vector2(transform.position.x, hit.point.y + 0.8f);
        yield return new PrecitionWait(TimeToStart, 1);
        Physics2D.Raycast(transform.position, Vector2.down * 99, 99, layerMask);
        playerpos = new Vector2((transform.position.x+playerpos.x)/2,Hit().point.y+ 0.8f) ;
        
        Spawn(canmove ? MovableWall : Wall, playerpos);
    }
    PlayerAttack playerAttack;
    void Spawn(Wall type, Vector2 pos)
    {
        walls.Add(Instantiate(type,(Vector3)pos,Quaternion.identity));
        clearDestroyed();
        if(walls.Count > 8 && !LevelUP.Items[26].IsTaken)
        {
            ClearWalls();
        }
    }
    RaycastHit2D Hit()
    {
        return Physics2D.Raycast(transform.position, Vector2.down* 99, 99, layerMask);
    }
    private static void clearDestroyed()
    {
        walls = walls.Where(v => v != null).ToList();
    }
    public static void ClearWalls()
    {
        clearDestroyed();
        int m = walls.Count;
        for(int i = 0; i < m; i++)
        {
            walls[0].Animator.SetTrigger("Destroy");
            walls.RemoveAt(0);
        }
    }
}
