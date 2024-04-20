using System.Collections;
using DustyStudios.MathAVM;
using UnityEngine;

public class BombAttack : AbstractAttack
{
    [SerializeField] private float Force;
    [SerializeField] private new Collider2D collider;

    protected override GameObject attack()
    {
        HoldCollectible collectible = GetComponent<HoldCollectible>();
        StartCoroutine(prodoljit());
        collectible.Rid();
        collectible.canPick = false;

        IEnumerator prodoljit()
        {
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            GetComponent<Rigidbody2D>().velocity =
                Vector2.right * MathA.OneOrNegativeOne(transform.lossyScale.x) * Force + Vector2.up * Force;
            yield return new WaitForSeconds(0.2f);
            collectible.canPick = true;
        }

        playerAttack.Animator.Play("ThrowThing");
        return gameObject;
    }
}