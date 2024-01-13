
using UnityEngine;

public class ExpCollectible : MonoBehaviour 
{
    [SerializeField] public float Exp;
    public void AddEXP()
    {
        Level.EXP+=Exp;
    } 
}
