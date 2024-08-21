using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public Stat Speed, Damage, Recharging;
    private void Start()
    {
        PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();
        playerAttack.Damage = Damage;
        playerAttack.TimeReload = Recharging;
        player.GetComponent<MoveBase>()._curentSpeed = Speed;
        foreach((Stat stat, TextAsImageGroup counter) stat in new[] { 
            (Speed, UiElementsList.instance.Counters.Speed),
            (Damage, UiElementsList.instance.Counters.Damage), 
            (Recharging, UiElementsList.instance.Counters.Recharge)})
            stat.stat.OnValueChanged += (o,n) => stat.counter.Text = n.ToString();
    }
}