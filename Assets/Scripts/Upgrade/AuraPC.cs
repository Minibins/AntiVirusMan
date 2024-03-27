using System.Collections;
using UnityEngine;

public class AuraPC : AbstractAura
{
    [SerializeField] private float Damage, SelfExpDamage;
    void OnEnable()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }
    protected override IEnumerator AuraAction()
    {
        MoveBase move;
        if(EnteredThings[0].TryGetComponent<MoveBase>(out move)) move.SetSpeedMultiplierForOllTime(0.8f);
        IDamageble health;
        if(EnteredThings[0].TryGetComponent<IDamageble>(out health))
        {
            Level.EXP -= SelfExpDamage;
            health.OnDamageGet(Damage,IDamageble.DamageType.Default);
        }
        if(health!=null||move!=null) yield return new WaitForSeconds(_ReloadTime);
    }
}
