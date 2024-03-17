using UnityEngine;

public class FirstAid : Collectible
{
    public override void Pick(GameObject picker)
    {
        picker.GetComponent<IHealable>().Heal(1);
        base.Pick(picker);
    }
}
