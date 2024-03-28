using DustyStudios.MathAVM;

using System.Collections;

using UnityEngine;
public class HoldCollectible : Collectible
{
    Rigidbody2D rb;
    [SerializeField] float collectionTime;
    [SerializeField] Vector2 holdPos;
    [SerializeField]new Collider2D collider;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public override void Pick(GameObject picker)
    {
        Transform parent = new GameObject().transform;
        transform.SetParent(parent,true);
        parent.SetParent(picker.transform,true);
        parent.localPosition = holdPos;
        transform.localPosition = Vector2.zero;
        rb.simulated = false;
         collider.isTrigger = true;
        StartCoroutine(rid());
        canPick = false;
        IEnumerator rid()
        {
            yield return new WaitForSeconds(collectionTime);
            Rid();
        }
    }
    public virtual void Rid()
    {
        rb.simulated = true;
        collider.isTrigger = false;
        canPick = true;
        Destroy(transform.parent.gameObject,0.1f);
        transform.SetParent(null);
    }
}
