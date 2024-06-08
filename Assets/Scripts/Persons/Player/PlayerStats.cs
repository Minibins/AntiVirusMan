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
        Move move = player.GetComponent<Move>();
        move._curentSpeed = Speed;
    }
}