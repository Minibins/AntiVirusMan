using System;
using System.Collections;
using DustyStudios.MathAVM;
using UnityEngine;

public class GreenKingMovement : MonoBehaviour, IDamageble
{
    [SerializeField] Transform PC, target;
    private new Transform transform;
    private Collider2D colider;
    [SerializeField] float defaultDashRange, damageDashReducing, reloadTime;
    private float dashRange, exp = 0;
    private bool canDamagePC;

    private Vector3 nextPos(Collider2D collider)
    {
        Vector3 colliderPos = new Vector3(transform.position.x + dashRange, collider.bounds.size.y / 2 - 5);
        while (Physics2D.OverlapPoint(colliderPos, 1 << 10))
        {
            colliderPos.y++;
        }

        return canDamagePC ? PC.position : new Vector3(transform.position.x + dashRange, colliderPos.y);
    }

    public void OnDamageGet(float Damage, IDamageble.DamageType type)
    {
        dashRange -= Damage * (1 - Convert.ToInt16(canDamagePC)) * damageDashReducing *
                     MathA.OneOrNegativeOne(PC.position.x < transform.position.x);
        canDamagePC = false;
        exp += Damage / 2;
        SetTargetPos();
    }

    IEnumerator Start()
    {
        PC = GetComponent<Enemy>()._PC.transform;
        Animator anim = GetComponent<Animator>();
        transform = base.transform;
        colider = GetComponent<Collider2D>();
        while (true)
        {
            anim.SetTrigger("Teleport");
            yield return new WaitForSeconds(reloadTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Throne"))
        {
            GetComponent<EnemyHealth>().OnDeath();
            collision.gameObject.GetComponentInParent<Animator>().SetTrigger("Die");
            Level.EXP += exp;
        }
    }

    public void Teleport()
    {
        transform.position = nextPos(colider);

        dashRange = defaultDashRange * MathA.OneOrNegativeOne(PC.position.x < transform.position.x);
        canDamagePC = PC.position.x < transform.position.x != PC.position.x < nextPos(colider).x;
        SetTargetPos();
        if (transform.position == PC.position)
        {
            PChealth.instance.ApplyDamage(1);
        }
    }

    private void SetTargetPos()
    {
        target.position = nextPos(colider);
    }
}