using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnersEnemy;
    [SerializeField] private GameObject[] Enemies;
    [SerializeField] private float minTimeSpawn;
    [SerializeField] private float maxTimeSpawn;
    public bool isSpawn;
    private void Start()
    {
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        while (isSpawn)
        {
            Instantiate(Enemies[Random.Range(0, Enemies.Length)], spawnersEnemy[Random.Range(0, spawnersEnemy.Length)].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
        }
    }

    public void StopOrStartSpawn()
    {
        isSpawn = !isSpawn;
        StartCoroutine(Spawn());
    }
}
