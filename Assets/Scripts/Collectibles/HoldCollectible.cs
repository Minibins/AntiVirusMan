using DustyStudios.MathAVM;

using UnityEngine;
public class HoldCollectible : Collectible
{
    [SerializeField] Vector2 holdPos;
    [SerializeField]new Collider2D collider;
    public override void Pick(GameObject picker)
    {
        Transform parent = new GameObject().transform;
        transform.SetParent(parent,true);
        parent.SetParent(picker.transform,true);
        parent.localPosition = holdPos;
        transform.localPosition = Vector2.zero;
        GetComponent<Rigidbody2D>().simulated = false;
         collider.isTrigger = true;
        enabled = false;
    }
}
