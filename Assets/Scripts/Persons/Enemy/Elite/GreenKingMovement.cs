using DustyStudios.MathAVM;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenKingMovement : MonoBehaviour, IDamageble
{
    [SerializeField] Transform PC, target;
    new Transform transform;
    [SerializeField] float defaultDashRange, damageDashReducing, reloadTime;
    float dashRange;
    bool canDamagePC;
    Vector3 nextPos
    {
        get => canDamagePC ? PC.position : transform.position + dashRange * Vector3.right;
    }
    public void OnDamageGet(int Damage,IDamageble.DamageType type)
    {
        canDamagePC = false;
        dashRange -= Damage*damageDashReducing*MathA.OneOrNegativeOne(PC.position.x<transform.position.x);
        SetTargetPos();
    }
    IEnumerator Start()
    {
        transform = base.transform;
        while(true)
        {
            dashRange = defaultDashRange * MathA.OneOrNegativeOne(PC.position.x < transform.position.x);
            canDamagePC = PC.position.x < transform.position.x != PC.position.x < nextPos.x;
            SetTargetPos();
            yield return new WaitForSeconds(reloadTime);
            transform.position = nextPos;
        }
    }
    void SetTargetPos()
    {
        target.position = nextPos;
    }
}