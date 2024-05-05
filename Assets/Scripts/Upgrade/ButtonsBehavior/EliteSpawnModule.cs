using UnityEngine;
public class EliteEnemyModule : UpgradeSpawnModule
{
    [SerializeField] private int enemyID;
    [SerializeField] private GameObject Enemy;
    [SerializeField] private int EliteSpawnChance;
    public override bool CanExecute(int ID, int spawnerID,int waveCount) => IsTaken&&ID==enemyID&&Random.Range(0,EliteSpawnChance)==0;
    public override void Spawn(int ID,GameObject spawnPoint) => Instantiate(Enemy, spawnPoint.transform.position, Quaternion.identity);
}