using System;
using UnityEngine;

public class EasterEgg : Collectible
{
    [SerializeField] private int ID;
    private int i;

    private void Start()
    {
        i = PlayerPrefs.GetInt("Egg2024inLocation_" + ID, 0);

        if (i <= 0)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        }
    }

    public override void Pick(GameObject picker)
    {
        base.Pick(picker);
        PlayerPrefs.SetInt("Egg2024inLocation_" + ID, i += 1);
    }

    public void DestoryEgg()
    {
        Destroy(gameObject);
    }
}