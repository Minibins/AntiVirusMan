using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPortals : Upgrade
{
    [SerializeField] private Portals[] Portals;
    [SerializeField] private GameObject[] spawnPointPortals;
    protected override void OnTake()
    {
        base.OnTake();

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            Portals Portals1 = Instantiate(Portals[0],
                spawnPointPortals[Random.Range(0, spawnPointPortals.Length)].transform.position+Vector3.forward*-5,
                Quaternion.identity);
            Portals Portals2 = Instantiate(Portals[1],
                    spawnPointPortals[Random.Range(0, spawnPointPortals.Length)].transform.position+Vector3.forward*-5, 
                    Quaternion.identity);
            Portals1.secondPortal = Portals2.gameObject;
            Portals2.secondPortal = Portals1.gameObject;

            yield return new PrecitionWait(10, 5);

            Destroy(Portals1);
            Destroy(Portals2);
        }
    }
}