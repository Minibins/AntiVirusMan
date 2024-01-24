using UnityEngine;

public class StarfallUpgrade : Upgrade
{
    protected override void OnTake()
    {
        base.OnTake();

        GameObject[] starfallObjects = GameObject.FindGameObjectsWithTag("Starfall");
        byte delay = 0;
        foreach(GameObject starfallObject in starfallObjects)
        {
            starfall starfallComponent = starfallObject.GetComponent<starfall>();
            if(starfallComponent != null) Invoke(StartStarfall(starfallComponent),((float)delay++) / 10);
        }
    }
    private string StartStarfall(starfall starfallComponent)
    {
        starfallComponent.StartCoroutine(starfallComponent.spawnStars());
        return "";
    }
}
