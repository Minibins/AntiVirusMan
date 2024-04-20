using UnityEngine;

public class bigexplosion : MonoBehaviour
{
    [SerializeField] private GameObject LittleExplosion;

    public void exploddde()
    {
        GameObject currentexpl = Instantiate(LittleExplosion,
            transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)),
            Quaternion.identity);
    }
}