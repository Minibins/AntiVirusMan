using System.Collections;
using UnityEngine;

public class InstantiateWall : MonoBehaviour
{
    [SerializeField] private GameObject Wall;
    [SerializeField] private float TimeToStart;
    public bool IsOpenly;

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
        Instantiate(Wall, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y-3.0917729f, 0), Quaternion.identity);
    }
}
