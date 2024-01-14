using System;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public static bool IsSelected;
    [SerializeField] private LevelUP LevelUpScript;
    [SerializeField] public static float EnemyNeedToUpLVL = 15;
    public static float EXP;
    private void Start()
    {
        EnemyNeedToUpLVL = 15;
        EXP = 0;
    }

    private void FixedUpdate()
    {
        var UI = UiElementsList.instance;
        UI.Counters.Lvl.fillAmount = (float)EXP / EnemyNeedToUpLVL;
        var LevelUpUI = UI.Panels.levelUpPanel.Panel;
        if(EXP >= EnemyNeedToUpLVL)
        {
            EXP = 0;

            LevelUpUI.SetActive(true);
            LevelUpScript.NewUpgrade();
        }

        if(IsSelected)
        {
            IsSelected = false;
            LevelUpUI.SetActive(false);
            Time.timeScale = 1;
        }
        if (Input.GetKey(KeyCode.F))
        {
            EXP +=4;
        }
    }
}
