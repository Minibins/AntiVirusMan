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
    public Image _enemyDieSprite;
    public float i;
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
        i = (float)EnemyDie / EnemyNeedToUpLVL;
        _enemyDieSprite.fillAmount = i;
        if (EnemyDie >= EnemyNeedToUpLVL)
        {
            EnemyDie = 0;
            
            LevelUpUI.SetActive(true);
            LevelUpScript.NewUpgrade();
        }
    }
}
