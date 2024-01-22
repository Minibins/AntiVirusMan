using UnityEngine;

public class DeathLine : PlayersCollisionChecker
{
    [SerializeField] Vector3 respawn;
    private void Start()
    {
        CollisionEnterAction += damagePlayer;
    }
    void damagePlayer()
    {
        foreach(var player in EnteredThings)
        {
            player.GetComponent<Player>().TakeDamage(respawn);
        }
    }
}
