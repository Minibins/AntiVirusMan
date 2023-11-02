using System;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject LevelUp;
    [SerializeField] private float EnemyNeedToUpLVL;
    private static float EnemyDie;
    public Image _enemyDieSprite;
    public float i;
    public Action OnEnemyDie { get; set; }
    private void Start()
    {
        EnemyDie = 0;
    }

    private void Update()
    {
        Upgrade();


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
        i = EnemyDie / Mathf.Round(EnemyNeedToUpLVL);
        _enemyDieSprite.fillAmount = i;
        if (EnemyDie >= EnemyNeedToUpLVL)
        {
            EnemyDie = 0;
            EnemyNeedToUpLVL *= 1.1f;
            //Level++;
            LevelUp.SetActive(true);
            LevelUp.GetComponent<LevelUP>().NewUpgrade();
        }
    }
}
