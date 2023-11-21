using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPortals : MonoBehaviour
{
    [SerializeField] private GameObject[] Portals;
    [SerializeField] private GameObject[] spawnPointPortals;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            StartSpawnPortals();
        }
    }

    public void StartSpawnPortals()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            GameObject Portals1 = Instantiate(Portals[0],
                spawnPointPortals[Random.Range(2, spawnPointPortals.Length)].transform.position,
                Quaternion.identity);

            GameObject Portals2 = Instantiate(Portals[1],
                new Vector3(
                    Random.Range(spawnPointPortals[0].transform.position.x, spawnPointPortals[1].transform.position.x),
                    spawnPointPortals[0].transform.position.y, 0), Quaternion.identity);

            Portals1.GetComponent<Portals>().secondPortal = Portals2;
            Portals2.GetComponent<Portals>().secondPortal = Portals1;

            yield return new WaitForSeconds(10);

            Destroy(Portals1);
            Destroy(Portals2);
        }
    }
}