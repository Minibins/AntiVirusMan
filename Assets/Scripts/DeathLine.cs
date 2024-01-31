using UnityEngine;

public class DeathLine : PlayersCollisionChecker
{
    [SerializeField] Vector3 respawn;
    private void Start()
    {
        EnterAction += damagePlayer;
    }
    void damagePlayer()
    {
        foreach(var player in EnteredThings)
        {
            Player.TakeDamage(respawn);
        }
    }
}
