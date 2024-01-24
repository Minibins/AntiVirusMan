using UnityEngine;
public class DroneUpgrade : Upgrade
{
    [SerializeField] GameObject Drone;
    protected override void OnTake()
    {
        base.OnTake();
        Transform Player = FindObjectOfType<Player>().transform;
        Instantiate(Drone,Player.position,Quaternion.identity,Player.parent);
    }
}
