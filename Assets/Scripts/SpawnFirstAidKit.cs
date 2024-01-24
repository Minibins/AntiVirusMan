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
    }
    
    private IEnumerator Spawn()
    {
        while (true)
        {
            GameObject _firstAidKit = Instantiate(firstAidKit,
                spawnPointFirstAidKit[UnityEngine.Random.Range(0, spawnPointFirstAidKit.Length)].transform.position,
                Quaternion.identity);
            yield return new WaitForSeconds(60);
            Destroy(_firstAidKit);
        }
    }
}