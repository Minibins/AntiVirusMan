using System;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] private LevelUP LevelUpScript;
    public static float EnemyNeedToUpLVL = 15;
    private static float eXP;
    public static float EXP { 
        get => eXP;
        set 
        { 
            eXP = value; 
            UiElementsList.instance.Counters.Lvl.fillAmount = (float)EXP / EnemyNeedToUpLVL;
            if(EXP >= EnemyNeedToUpLVL)
            {
                EXP = 0;
                LevelUP.instance.NewUpgrade();
            }
        }
    }

    private void Start()
    {
        EXP = 0;
        EnemyNeedToUpLVL = 15;
    }

    private void FixedUpdate()
    {
        #if UNITY_EDITOR
        if (Input.GetKey(KeyCode.F))
        {
            EXP +=4;
        }
        #endif
    }
}
