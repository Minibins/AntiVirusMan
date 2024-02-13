using System.Collections;
using UnityEngine;
public class StarfallUpgrade : Upgrade
{
    protected override void OnTake()
    {
        base.OnTake();
        StartCoroutine(StartStarfall(GameObject.FindObjectsOfType<starfall>()));
    }
    private IEnumerator StartStarfall(starfall[] starfallObjects)
    {
        foreach(starfall starfallObject in starfallObjects)
        {
            starfall starfallComponent = starfallObject.GetComponent<starfall>();
            starfallObject.StartCoroutine(nameof(starfallObject.spawnStars));
            yield return new WaitForSeconds(0.15f);
        }
    }
}
