using UnityEngine;

public class FixedProgressionSpawnModule : FixedProgressionUpgrade, ISpawnerModule
{
    [SerializeField] private int enemyID;
    [SerializeField] private GameObject Enemy;
    [SerializeField] private int EliteSpawnChance;
    [SerializeField] protected SpawnerEnemy spawner;
    protected virtual void Awake() => spawner.spawnerModules.Add(this);
    public bool CanExecute(int ID,int SpawnerID,int waveCount) => EasterEgg.isEaster && isTaken && ID == enemyID && Random.Range(0,EliteSpawnChance) == 0;
    public void Spawn(int ID,GameObject spawnPoint) => Instantiate(Enemy,spawnPoint.transform.position,Quaternion.identity);
}