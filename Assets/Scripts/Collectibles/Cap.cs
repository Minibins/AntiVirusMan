using UnityEngine;

public class Cap : HoldCollectible
{
    public override void Pick(GameObject picker)
    {
        base.Pick(picker);
        transform.parent.parent.gameObject.layer = 18;
    }

    public override void Rid()
    {
        transform.parent.parent.gameObject.layer = 9;
        Destroy(gameObject);
        Destroy(transform.parent.gameObject);
    }
}