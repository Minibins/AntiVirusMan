using UnityEngine;

public class GameWorldUpgrade : Collectible
{
    [SerializeField] int ID;
    public override void Pick(GameObject picker)
    {
        base.Pick(picker);
        LevelUP.GetItem(ID);
    }
}
