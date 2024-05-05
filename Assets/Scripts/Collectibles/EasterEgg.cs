using System;
using UnityEngine;

public class EasterEgg : Collectible
{
    [SerializeField] private int ID;
    private int i;

    protected Color color =>new Color(1f,1f,1f,i <= 0 ? 1f : 0.5f);

    private void Start()
    {
        i = PlayerPrefs.GetInt("Egg2024inLocation_" + ID, 0);
        foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
            renderer.color = color;
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