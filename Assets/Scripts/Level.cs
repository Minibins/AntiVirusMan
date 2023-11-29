using System;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public static bool IsSelected;
    [SerializeField] private LevelUP LevelUpScript;
    [SerializeField] public static float EnemyNeedToUpLVL = 15;
    private static float EnemyDie;
    private void Start()
    {
        EnemyNeedToUpLVL = 15;
        EnemyDie = 0;
    }

    private void FixedUpdate()
    {
        var UI = UiElementsList.instance;
        UI.Counters.Lvl.fillAmount = (float)EnemyDie / EnemyNeedToUpLVL;
        var LevelUpUI = UI.Panels.levelUpPanel.Panel;
        if(EnemyDie >= EnemyNeedToUpLVL)
        {
            EnemyDie = 0;

            
            LevelUpUI.SetActive(true);
            LevelUpScript.NewUpgrade();
        }

        if(IsSelected)
        {
            IsSelected = false;
            LevelUpUI.SetActive(false);
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            EnemyDie = 10000000;
        }
    }

    public static void TakeEXP(float kills)
    {
        EnemyDie += kills;
    }

}
