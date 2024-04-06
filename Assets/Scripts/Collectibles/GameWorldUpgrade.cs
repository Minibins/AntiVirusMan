using UnityEngine;

public class GameWorldUpgrade : Collectible
{
    [SerializeField] int ID;
    public override void Pick(GameObject picker)
    {
        base.Pick(picker);
        if(!LevelUP.Items[ID].IsTaken) LevelUP.GetItem(ID);
    }
}
