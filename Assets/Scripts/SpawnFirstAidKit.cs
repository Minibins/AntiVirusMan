using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFirstAidKit : MonoBehaviour
{
    [SerializeField] private GameObject firstAidKit;
    [SerializeField] private GameObject[] spawnPointFirstAidKit;
    
    public void StartSpawnAid()
    {
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