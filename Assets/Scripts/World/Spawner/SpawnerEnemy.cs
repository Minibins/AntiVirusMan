using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnersEnemy, spawnersBoss, spawnersWireEnemy;
    [SerializeField] private EnemySpawn[] enemies, wireEnemies;
    [SerializeField] private Animator[] spawnersAnim;
    [SerializeField] private GameObject Boss;
    [SerializeField] private bool BossSpawned;
    [SerializeField] private float minTimeSpawn, maxTimeSpawn, minTimeSpawnWave, maxTimeSpawnWave,shotsSpawn,shotsWave;
    public bool isSpawn;
    public static float SpawnCoeficient = 1f;
    public List<ISpawnerModule> spawnerModules = new List<ISpawnerModule>();
    [SerializeField] private Vector2 WaveSizeRange;
    public static GameObject[] GetPosssibleEnemies(EnemySpawn[] enemies) => enemies.Where(e => e.spawnBounds.isInBounds(Timer.time)).Select(e=>e.prefab).ToArray();
    public EnemySpawn[] Enemies
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
                    GameObject[] possibleWireEnemies = GetPosssibleEnemies(wireEnemies);
                    GameObject WireEnemy = Instantiate(possibleWireEnemies[Random.Range(0, possibleWireEnemies.Length)],
                        spawnersWireEnemy[i].transform.position, Quaternion.identity);
                WireEnemy wireEnemy;
                if(WireEnemy.TryGetComponent<WireEnemy>(out wireEnemy)) wireEnemy.MoveToPoint = spawnersWireEnemy[i];
                    break;
            }

            yield return new PrecitionWait(Mathf.Max(
                Random.Range(minTimeSpawnWave, maxTimeSpawnWave) -
                Mathf.Lerp(0, maxTimeSpawnWave, (float) Timer.time / 600), 0.1f),shotsSpawn);
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
        yield return new PrecitionWait(0.7f, 1);
        spawnersAnim[spawnPoint].SetTrigger("Spawn");
        GameObject[] possibleEnemies = GetPosssibleEnemies(enemies);
        int enemyID = Random.Range(0, possibleEnemies.Length);
        foreach(ISpawnerModule module in spawnerModules)
        {
            if(!module.CanExecute(enemyID,spawnPoint,waveCount)) continue;
            module.Spawn(enemyID,spawnersEnemy[spawnPoint]);
            yield break;
        }
        Instantiate(possibleEnemies[enemyID], spawnersEnemy[spawnPoint].transform.position, Quaternion.identity);
    }
    [System.Serializable]
    public struct EnemySpawn
    {
        public GameObject prefab;
        public ValueBounds spawnBounds;
    }
}