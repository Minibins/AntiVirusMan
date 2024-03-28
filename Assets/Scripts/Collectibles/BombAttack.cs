using DustyStudios.MathAVM;

using System.Collections;

using UnityEngine;

public class BombAttack : AbstractAttack
{
    [SerializeField] float Force;
    [SerializeField] new Collider2D collider;
    protected override GameObject attack()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.simulated = true;
        StartCoroutine(prodoljit());
        Destroy(transform.parent.gameObject,0.1f);
        transform.SetParent(null);
        IEnumerator prodoljit()
        {
            yield return new WaitForFixedUpdate(); yield return new WaitForFixedUpdate();
            rb.velocity = Vector2.right*MathA.OneOrNegativeOne(transform.lossyScale.x)*Force;
            yield return new WaitForSeconds(0.1f);
            collider.isTrigger = false;
            GetComponent<HoldCollectible>().enabled = true;
        }
        return gameObject;
    }
}