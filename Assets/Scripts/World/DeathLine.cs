using UnityEngine;

public class DeathLine : PlayersCollisionChecker
{
    [SerializeField] private Vector3 respawn;

    private void Start()
    {
        EnterAction += damagePlayer;
    }

    private void damagePlayer()
    {
        foreach (var player in EnteredThings)
        {
            Player.TakeDamage(respawn);
        }
    }
}