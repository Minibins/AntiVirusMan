using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnersEnemy, spawnersBoss, enemies, WireEnemies, spawnersWireEnemy;
    [SerializeField] private Animator[] spawnersAnim;
    [SerializeField] private GameObject Boss;
    [SerializeField] private bool BossSpawned;
    [SerializeField] private float minTimeSpawn, maxTimeSpawn, minTimeSpawnWave, maxTimeSpawnWave,shotsSpawn,shotsWave;
    public bool isSpawn;
    public static float SpawnCoeficient = 1f;
    public List<ISpawnerModule> spawnerModules = new List<ISpawnerModule>();
    [SerializeField] private Vector2 WaveSizeRange;

    public GameObject[] Enemies
    {
        get => enemies;
        private set => enemies = value;
    }

    private void Start()
    {
        Enemy.Enemies.Clear();
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
                        UiElementsList.instance.Panels.BossPanel.SetActive(true);
                    Destroy(UiElementsList.instance.Panels.BossPanel, 10f);
                        BossSpawned = true;
                    }

                    break;

                case < 10:
                    int spawnPoint = Random.Range(0, spawnersEnemy.Length);
                    int enemiesInWave = (int) Random.Range(WaveSizeRange.x, WaveSizeRange.y);
                    for (int X = 0; X < enemiesInWave;)
                    {
                        yield return WaitSpawnTime();
                        StartCoroutine(SpawnEnemy(spawnPoint, X++));
                    }

                    int i = Random.Range(0, spawnersWireEnemy.Length);
                    GameObject WireEnemy = Instantiate(WireEnemies[Random.Range(0, WireEnemies.Length)],
                        spawnersWireEnemy[i].transform.position, Quaternion.identity);
                WireEnemy wireEnemy;
                if(WireEnemy.TryGetComponent<WireEnemy>(out wireEnemy)) wireEnemy.MoveToPoint = spawnersWireEnemy[i];
                    break;
            }

            yield return new PrecitionWait(Mathf.Max(
                Random.Range(minTimeSpawnWave, maxTimeSpawnWave) -
                Mathf.Lerp(0, maxTimeSpawnWave, (float) Timer.time / 600), 0),shotsSpawn);
        }
    }

    IEnumerator WaitSpawnTime()
    {
        yield return new PrecitionWait((Random.Range(minTimeSpawn, maxTimeSpawn) -
                                         Mathf.Lerp(0, (maxTimeSpawn - minTimeSpawn) / 2, (float) Timer.time / 600)) /
                                        SpawnCoeficient,shotsSpawn);
    }

    public void StopOrStartSpawn()
    {
        isSpawn = !isSpawn;
        StartCoroutine(Spawn());
    }

    IEnumerator SpawnEnemy(int spawnPoint, int waveCount)
    {
        yield return new PrecitionWait(Time.fixedDeltaTime, 1);
        spawnersAnim[spawnPoint].SetTrigger("Spawn");
        yield return new WaitForSeconds(0.7f);
        int enemyID = Random.Range(0, Enemies.Length);
        foreach (ISpawnerModule module in spawnerModules)
        {
            if (module.CanExecute(enemyID, spawnPoint, waveCount))
            {
                module.Spawn(enemyID, spawnersEnemy[spawnPoint]);
                yield break;
            }
        }

        Instantiate(Enemies[enemyID], spawnersEnemy[spawnPoint].transform.position, Quaternion.identity);
    }
}