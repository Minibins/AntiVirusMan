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
    }
}