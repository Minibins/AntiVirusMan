using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnersEnemy;
    [SerializeField] private GameObject[] Enemies;
    [SerializeField] private GameObject[] WireEnemies;
    [SerializeField] private GameObject[] spawnersWireEnemy;
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
            int i = Random.Range(0, 100);
            switch (i)
            {
                case >= 30:
                    Instantiate(Enemies[Random.Range(0, Enemies.Length)], spawnersEnemy[Random.Range(0, spawnersEnemy.Length)].transform.position, Quaternion.identity);
                    break;
                case <= 30:
                    Instantiate(WireEnemies[Random.Range(0, WireEnemies.Length)], spawnersWireEnemy[Random.Range(0, spawnersWireEnemy.Length)].transform.position, Quaternion.identity);
                    break;
            }
            yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
        }
    }

    public void StopOrStartSpawn()
    {
        isSpawn = !isSpawn;
        StartCoroutine(Spawn());
    }
}
