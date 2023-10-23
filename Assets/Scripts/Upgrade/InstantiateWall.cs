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
    {
        yield return new WaitForSeconds(TimeToStart);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down * 99, 99, layerMask); // Тут рейкаст
        if (canmove) Instantiate(MovableWall, new Vector3(transform.position.x, hit.point.y + 0.8f, transform.position.z), Quaternion.identity);
        else Instantiate(Wall, new Vector3(transform.position.x, hit.point.y + 0.8f, transform.position.z), Quaternion.identity);
        
    }
}
