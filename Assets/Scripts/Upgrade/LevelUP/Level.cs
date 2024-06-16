using DustyStudios;
using System;
using System.Collections.Generic;
using UnityEngine;
public class Level : MonoBehaviour
{
    private static MultiRechargingValue eXP = new(new(15,0),0,time=>new WaitForSeconds(time));
    public static List<MultiRechargingValue.RechargeStream> RechargeStreams => eXP.rechargeStreams;
    public static Action onNegativeExp;
    public static float EXP { 
        get => eXP;
        set 
        { 
            eXP.Value = value; 
            UiElementsList.instance.Counters.Lvl.fillAmount = (float)EXP / EnemyNeedToUpLVL;
            if(EXP >= EnemyNeedToUpLVL)
            {
                EXP = 0;
                LevelUP.instance.NewUpgrade();
                EnemyNeedToUpLVL*= 1.04137974f;
            }
            if(eXP < 0)
            {
                onNegativeExp.Invoke();
                eXP.Value = EnemyNeedToUpLVL - 1;
            }
        }
    }

    public static float EnemyNeedToUpLVL { get => eXP.bounds.max; set => eXP.bounds.max = value; }

    private void Start()
    {
        EXP = 0;
        EnemyNeedToUpLVL = 15;
        onNegativeExp = new(()=> Player.TakeDamage());
    }

    private void FixedUpdate()
    {
        #if UNITY_EDITOR
        if (Input.GetKey(KeyCode.F))
        {
            EXP +=4;
            EnemyNeedToUpLVL = 8;
        }
        #endif
    }
    [DustyConsoleCommand("exp", "Set EXP into value", typeof(float))]
    public static string SetHealth(int exp)
    {
        EXP = exp;
        return $"Expirience are {exp} now";
    }
}