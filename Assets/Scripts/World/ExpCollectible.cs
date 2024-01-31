
using UnityEngine;

public class ExpCollectible : PlayersCollisionChecker 
{
    private void Start()
    {
        EnterAction += AddEXP;
    }
    [SerializeField] public float Exp;
    public void AddEXP()
    {
        Level.EXP+=Exp;
    } 
}
