using System.Collections;
using UnityEngine;
public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnersEnemy;
    [SerializeField] private GameObject[] spawnersBoss;
    [SerializeField] private GameObject Boss;
    [SerializeField] private bool BossSpawned;
    [SerializeField] private GameObject[] Enemies;
    [SerializeField] private GameObject[] EnemiesV2;
    [SerializeField] private GameObject[] WireEnemies;
    [SerializeField] private GameObject[] spawnersWireEnemy;
    [SerializeField] private float minTimeSpawn;
    [SerializeField] private float maxTimeSpawn;
    [SerializeField] private float minTimeSpawnWave;
    [SerializeField] private float maxTimeSpawnWave;
    [SerializeField] private GameManager GM;
    public bool isSpawn;
    private void Start()
    {
        BossSpawned = false;
        StartCoroutine(Spawn());
    }


    IEnumerator Spawn()
    {
        while (isSpawn)
        {
            switch (GM.min)
            {
                case >= 10:
                    if (GM.min >= 10 && BossSpawned == false)
                    {
                        Instantiate(Boss, spawnersBoss[Random.Range(0, spawnersBoss.Length)].transform.position, Quaternion.identity);
                        print("Boss has spawned");
                        BossSpawned=true;
                    }
                    break;


                case > 5 :
                    if (GM.min < 10 && BossSpawned == false)
                    {
                        int spawnPoint2 = Random.Range(0, spawnersEnemy.Length);
                        SpawnEnemy(1, spawnPoint2);
                        yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
                        SpawnEnemy(1, spawnPoint2);
                        yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
                        SpawnEnemy(1, spawnPoint2);
                        yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
                    }
                    break;

                case < 5:
                    if (GM.min <= 5 && BossSpawned == false)
                    {
                        int spawnPoint = Random.Range(0, spawnersEnemy.Length);
                        SpawnEnemy(0, spawnPoint);
                        yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
                        SpawnEnemy(0, spawnPoint);
                        yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
                        SpawnEnemy(0, spawnPoint);
                        yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
                    }
                    
                    int i = Random.Range(0, spawnersWireEnemy.Length);
                    GameObject WireEnemy = Instantiate(WireEnemies[Random.Range(0, WireEnemies.Length)], spawnersWireEnemy[i].transform.position, Quaternion.identity);
                    WireEnemy.GetComponent<Enemy>().MoveToPoint = spawnersWireEnemy[i];
                    break;
            }

            yield return new WaitForSeconds(Random.Range(minTimeSpawnWave, maxTimeSpawnWave));
        }
    }

    public void StopOrStartSpawn()
    {
        isSpawn = !isSpawn;
        StartCoroutine(Spawn());
    }

    private void SpawnEnemy(int wave, int spawnPoint)
    {
        switch (wave)
        {
            case 0:
                Instantiate(Enemies[Random.Range(0, Enemies.Length)], spawnersEnemy[spawnPoint].transform.position, Quaternion.identity);
                break;

            case 1:
                Instantiate(EnemiesV2[Random.Range(0, EnemiesV2.Length)], spawnersEnemy[spawnPoint].transform.position, Quaternion.identity);
                break;
        }
    }
}
