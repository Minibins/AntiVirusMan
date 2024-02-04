using UnityEngine;
public class EliteEnemyModule : Upgrade, ISpawnerModule
{
    private void Awake()
    {
        spawner.spawnerModules.Add(this);
    }
    [SerializeField] int enemyID;
    [SerializeField] GameObject Enemy;
    [SerializeField] SpawnerEnemy spawner;
    [SerializeField] int EliteSpawnChance;
    public bool CanExecute(int ID, int spawnerID)
    {
        return IsTaken&&ID==enemyID&&Random.Range(0,EliteSpawnChance)==0;
    }

    public void Spawn(GameObject spawnPoint)
    {
        Instantiate(Enemy, spawnPoint.transform.position, Quaternion.identity);
    }
}