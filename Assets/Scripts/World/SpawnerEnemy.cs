using System.Collections;
using UnityEngine;
public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnersEnemy;
    [SerializeField] private Animator[] spawnersAnim;
    [SerializeField] private GameObject[] spawnersBoss;
    [SerializeField] private GameObject Boss;
    [SerializeField] private bool BossSpawned;
    [SerializeField] private GameObject[] Enemies, EnemiesV2, WireEnemies, spawnersWireEnemy;
    [SerializeField] private int[] EliteID;
    [SerializeField] private float minTimeSpawn, maxTimeSpawn, minTimeSpawnWave, maxTimeSpawnWave;
    [SerializeField] private GameObject BossAlpha;
    [SerializeField] private int EliteSpawnChance;
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
            switch (Timer.min)
            {
                case >= 10:
                    if (Timer.min >= 10 && BossSpawned == false)
                    {
                        Instantiate(Boss, spawnersBoss[Random.Range(0, spawnersBoss.Length)].transform.position,
                            Quaternion.identity);
                        print("Boss has spawned");
                        BossAlpha.SetActive(true);
                        BossSpawned = true;
                    }

                    break;


                case > 5:
                    if (Timer.min < 10 && BossSpawned == false)
                    {
                        int spawnPoint2 = Random.Range(0, spawnersEnemy.Length);
                        StartCoroutine(SpawnEnemy(spawnPoint2));
                        yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
                        StartCoroutine(SpawnEnemy(spawnPoint2));
                        yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
                        StartCoroutine(SpawnEnemy(spawnPoint2));
                        yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
                    }

                    break;

                case < 5:
                    if (Timer.min <= 5 && BossSpawned == false)
                    {
                        int spawnPoint = Random.Range(0, spawnersEnemy.Length);
                        StartCoroutine(SpawnEnemy(spawnPoint));
                        yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
                        StartCoroutine(SpawnEnemy(spawnPoint));
                        yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
                        StartCoroutine(SpawnEnemy(spawnPoint));
                        yield return new WaitForSeconds(Random.Range(minTimeSpawn, maxTimeSpawn));
                    }

                    int i = Random.Range(0, spawnersWireEnemy.Length);
                    GameObject WireEnemy = Instantiate(WireEnemies[Random.Range(0, WireEnemies.Length)],
                        spawnersWireEnemy[i].transform.position, Quaternion.identity);
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

    IEnumerator SpawnEnemy(int spawnPoint)
    {
        spawnersAnim[spawnPoint].SetTrigger("Spawn");
        yield return new WaitForSeconds(0.7f);
        int enemy = Random.Range(0, Enemies.Length);
        int Iselite = Random.Range(0, EliteSpawnChance);
        try
        {
            if (LevelUP.isTaken[EliteID[enemy]] && Iselite == 0)
            {
                Instantiate(EnemiesV2[Random.Range(0, Enemies.Length)], spawnersEnemy[spawnPoint].transform.position,
                    Quaternion.identity);
            }
            else
            {
                Instantiate(Enemies[Random.Range(0, Enemies.Length)], spawnersEnemy[spawnPoint].transform.position,
                    Quaternion.identity);
            }
        }
        catch
        {
            Debug.Log("Enemy:" + enemy + " IsElite:" + Iselite + " IsTaken:" + LevelUP.isTaken.ToString() +
                      " EliteID:" + EliteID[enemy]);
        }
    }
}