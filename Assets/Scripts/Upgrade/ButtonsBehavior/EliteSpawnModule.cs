using UnityEngine;
public class EliteEnemyModule : UpgradeSpawnModule
{
    [SerializeField] int enemyID;
    [SerializeField] GameObject Enemy;
    [SerializeField] int EliteSpawnChance;
    public override bool CanExecute(int ID, int spawnerID,int waveCount)
    {
        return IsTaken&&ID==enemyID&&Random.Range(0,EliteSpawnChance)==0;
    }

    public override void Spawn(int ID,GameObject spawnPoint)
    {
        Instantiate(Enemy, spawnPoint.transform.position, Quaternion.identity);
    }
}