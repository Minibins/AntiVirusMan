
using UnityEngine;

public class ExpCollectible : PlayersCollisionChecker 
{
    private void Start()
    {
        CollisionEnterAction += AddEXP;
    }
    [SerializeField] public float Exp;
    public void AddEXP()
    {
        Level.EXP+=Exp;
    } 
}
