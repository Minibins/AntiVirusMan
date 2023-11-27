using System;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public static bool IsSelected;
    [SerializeField] private GameObject LevelUpUI;
    [SerializeField] private LevelUP LevelUpScript;
    [SerializeField] public static float EnemyNeedToUpLVL = 15;
    private static float EnemyDie;
    [SerializeField] private Image _enemyDieSprite;
    public Action OnEnemyDie { get; set; }
    private void Start()
    {
        EnemyNeedToUpLVL = 15;
        EnemyDie = 0;
    }

    private void Update()
    {
        Upgrade();

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

    private void OnDisable()
    {
        OnEnemyDie = null;
    }

    private void Upgrade() 
    {
        _enemyDieSprite.fillAmount = (float)EnemyDie / EnemyNeedToUpLVL;
        if (EnemyDie >= EnemyNeedToUpLVL)
        {
            EnemyDie = 0;
            
            LevelUpUI.SetActive(true);
            LevelUpScript.NewUpgrade();
        }
    }
}
