using DustyStudios.MathAVM;

using UnityEngine;

public class BigShieldAttack : ShieldAttack
{
    private Player player;
    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<Player>();
        if(player == null) player = GetComponentInParent<Player>();
    }
    protected override GameObject attack()
    {
        GameObject weapon = base.attack();
        player.Dash(-MathA.OneOrNegativeOne(transform.lossyScale.x) * (!LevelUP.Items[10].IsTaken ? 0.8f : 2.5f));
        return weapon;
    }
}