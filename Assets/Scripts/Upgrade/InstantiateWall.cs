using System.Collections;
using UnityEngine;

public class InstantiateWall : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject Wall;
    [SerializeField] private GameObject MovableWall;
    [SerializeField] private float TimeToStart;
    public bool IsOpenly;
    public bool canmove;


    public void OnJump()
    {
        if (IsOpenly == true)
        {
            StartCoroutine(SpawnWall());
        }
    }
    IEnumerator SpawnWall()
    {   RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down * 99, 99, layerMask); // Тут рейкаст
        Vector2 playerpos=new Vector2(transform.position.x, hit.point.y + 0.8f);
        yield return new WaitForSeconds(TimeToStart);
        hit = Physics2D.Raycast(transform.position, Vector2.down * 99, 99, layerMask);
        playerpos = new Vector2((transform.position.x+playerpos.x)/2,hit.point.y+ 0.8f) ;
        if(canmove) Instantiate(MovableWall,playerpos,Quaternion.identity);
        else Instantiate(Wall,playerpos,Quaternion.identity);
        
    }
}
