using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

public class NumberOfObjectsIsDamage : Upgrade
{
    private PlayerAttack playerAttack;
    [SerializeField] private string[] objectsTag;
    [SerializeField] private float[] multiplerDamage;
    public static bool[] tagAlloved;
    private void Start()
    {
        tagAlloved=new bool[objectsTag.Length];
        playerAttack=GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
    }
    protected override void OnTake()
    {
        base.OnTake();
        tagAlloved[0]=true;
    }
    public void ChangeDamage()
    {
        playerAttack.coefficientAttack[1] = 0f;
        sbyte iterateon=-1;
        foreach (var tag in objectsTag)
        {
            if(!tagAlloved[++iterateon]) {
                continue;
            }
            playerAttack.coefficientAttack[1]+= GameObject.FindGameObjectsWithTag(tag).Length * multiplerDamage[iterateon];
        }
        Debug.Log(playerAttack.coefficientAttack[1]);
    }
}