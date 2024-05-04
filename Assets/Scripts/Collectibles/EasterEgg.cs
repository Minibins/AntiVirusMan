using UnityEngine;
public class EasterEgg : Collectible
{
    [SerializeField] private int ID;
    public override void Pick(GameObject picker)
    {
        base.Pick(picker);
        PlayerPrefs.SetInt("EasterEgg"+ID, 1);
    }
}