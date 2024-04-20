using System.Collections;
using DustyStudios.MathAVM;
using UnityEngine;

public class StrelaushaaShtuka : MonoBehaviour
{
    [SerializeField] GameObject bulletAsset;

    IEnumerator Start()
    {
        int rotation = 0;
        while (true)
        {
            rotation += 5;
            yield return new WaitForSeconds(0.1f);
            GameObject bullet = Instantiate(bulletAsset, transform.position, transform.rotation);
            bullet.transform.position = MathA.RotatedVector(Vector2.up, rotation);
            bullet.transform.rotation = MathA.VectorsAngle(bullet.transform.position);
        }
    }
}