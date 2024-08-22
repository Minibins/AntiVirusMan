using UnityEngine;

public class MathyrSpawnModule : UpgradeSpawnModule
{
    [SerializeField] private GameObject[] Enemies;
    protected override void Awake()
    {
        base.Awake();
        if(spawner.Enemies.Length != Enemies.Length) throw new System.Exception("Списки мобов не совпадают по количеству");
    }
    public override bool CanExecute(int ID,int SpawnerID,int waveCount) => Random.Range(0,2) == 0 && IsTaken;
    public override void Spawn(int ID,GameObject spawnPoint) => Instantiate(Enemies[ID],spawnPoint.transform.position,Quaternion.identity);
}
