using DustyStudios.MathAVM;
using System.Collections;
using UnityEngine;

public class BombAttack : AbstractAttack
{
    [SerializeField] float Force;
    [SerializeField] new Collider2D collider;
    protected override GameObject attack()
    {
        StartCoroutine(prodoljit());
        GetComponent<HoldCollectible>().Rid();
        IEnumerator prodoljit()
        {
            yield return new WaitForFixedUpdate(); yield return new WaitForFixedUpdate();
            GetComponent<Rigidbody2D>().velocity = Vector2.right*MathA.OneOrNegativeOne(transform.lossyScale.x)*Force;
            yield return new WaitForSeconds(0.1f);
        }
        return gameObject;
    }
}