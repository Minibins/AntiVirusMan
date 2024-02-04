using UnityEngine;
public interface ISpawnerModule
{
    public bool CanExecute(int ID, int SpawnerID);
    public void Spawn(GameObject spawnPoint);
}
