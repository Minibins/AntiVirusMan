using System;
using UnityEngine;

public class LevelupManager : MonoBehaviour
{
    [SerializeField] private GameObject LevelUp;
    [SerializeField] private float EnemyNeedToUpLVL;
    private static float EnemyDie;
    private string _enemyDieText;
    public Action OnEnemyDie { get; set; }
    public string EnemyDieText
    {
        get
        {
            return _enemyDieText;
        }
        set
        {
            _enemyDieText = value;
            OnEnemyDie?.Invoke();
        }
    }

    private void Update()
    {
        Upgrade();
    }

    public static void TakeEXP(int kills)
    {
        EnemyDie += kills;
    }

    private void OnDisable()
    {
        OnEnemyDie = null;
    }

    private void Upgrade()
    {
        EnemyDieText = "EnemyDie:" + EnemyDie.ToString() + "/" + Mathf.Round(EnemyNeedToUpLVL).ToString();
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
