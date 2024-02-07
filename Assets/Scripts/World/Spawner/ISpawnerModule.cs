using UnityEngine;
public interface ISpawnerModule
{
    public bool CanExecute(int ID, int SpawnerID, int waveCount);
    public void Spawn(int ID, GameObject spawnPoint);
}
