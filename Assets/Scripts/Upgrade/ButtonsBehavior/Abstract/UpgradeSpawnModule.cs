using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpawnModule : Upgrade, ISpawnerModule
{
    [SerializeField] protected SpawnerEnemy spawner;
    protected virtual void Awake()
    {
        spawner.spawnerModules.Add(this);
    }
    public virtual bool CanExecute(int ID,int SpawnerID,int waveCount)
    {
        return IsTaken;
    }
    public virtual void Spawn(int ID,GameObject spawnPoint)
    {
        throw new NotImplementedException();
    }
}
