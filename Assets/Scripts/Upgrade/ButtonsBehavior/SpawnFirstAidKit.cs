using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFirstAidKit : Upgrade
{
    [SerializeField] private GameObject firstAidKit;
    [SerializeField] private GameObject[] spawnPointFirstAidKit;
    protected override void OnTake()
    {
        base.OnTake();
        StartCoroutine(Spawn());
        StartCoroutine(Steal());
    }
    
    private IEnumerator Spawn()
    {
        while (true)
        {
            GameObject _firstAidKit = Instantiate(firstAidKit,
                spawnPointFirstAidKit[UnityEngine.Random.Range(0, spawnPointFirstAidKit.Length)].transform.position,
                Quaternion.identity);
            if(LevelUP.IsItemTaken(1))
                _firstAidKit.AddComponent<DRAG>();
            yield return new PrecitionWait(8,4);
            Destroy(_firstAidKit);
        }
    }
    private IEnumerator Steal()
    {
        while (true)
        {
            yield return new PrecitionWait(8,4);
            Level.EXP-=0.5f;
        }
    }
}