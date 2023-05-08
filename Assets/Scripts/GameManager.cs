using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    // [SerializeField] private GameObject LevelUp;
    [SerializeField] private GameObject LosePanel;
    public GameObject SettingsPanel;
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private GameObject LevelUp;
    //[SerializeField] private int Level;
    //[SerializeField] private float Health;
    [SerializeField] private int sec;
    [SerializeField] private int min;
    [SerializeField] private int TimeToWin;
    [SerializeField] private float EnemyNeedToUpLVL;
    private static float EnemyDie;
    [SerializeField] private bool StopTime = true;
    private string _enemyDieText;
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
    private string _timerText;
    public string TimerText
    {
        get
        {
            return _timerText;
        }
        set
        {
            _timerText = value;
            OnTimer?.Invoke();
        }
    }
    [SerializeField] private Text LiveTextLose;
    [SerializeField] private SpawnerEnemy SE;
    [SerializeField] private Save save;
    [SerializeField] private Animator anim;
    [SerializeField] private int _level;
    public Action OnTimer { get; set; }
    public Action OnEnemyDie { get; set; }
    private void Start()
    {
        Settings();
        StartCoroutine(TimeFlow());
    }

    private void Update()
    {
        Upgrade();
    }
    public void TakeDamage(float Damage)
    {
        if (Damage > 0)
        {
            return;
        }
        //Health -= Damage;
        //Health_Text.text = "Health:" + Health;
        //if (Health <= 0)
        //{
        Destroy(Player);
        //anim.SetBool("Lose", true);
        SE.GetComponent<SpawnerEnemy>().StopOrStartSpawn();
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemy.Length; i++)
        {
            Destroy(enemy[i]);
        }
        LosePanel.SetActive(true);
        StopTime = false;
        //}
    }
    public static void TakeEXP(int kills)
    {
        EnemyDie += kills;
    }
    IEnumerator TimeFlow()
    {
        while (StopTime == true)
        {
            if (sec == 59)
            {
                min++;
                sec = -1;
                if (min >= TimeToWin)
                {
                    save.LoadField();
                    if (save.data.WinLocation < _level + 1)
                    {
                        save.data.WinLocation = _level + 1;
                        save.SaveField();
                        print("save");
                    }
                }
            }
            sec++;
            TimerText = min.ToString("D2") + " : " + sec.ToString("D2");
            LiveTextLose.text = "You live:" + min.ToString("D2") + " : " + sec.ToString("D2");
            yield return new WaitForSeconds(1);
        }
    }
    private void OnDisable()
    {
        OnTimer = null;
        OnEnemyDie = null;
    }
    public void MoveScene(string _Scene)
    {
        SceneManager.LoadScene(_Scene);
    }

    public void OpenSettings(bool Open)
    {
        SettingsPanel.SetActive(Open);
        //TimeFlow();
    }
    private void Settings()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        SettingsPanel.SetActive(false);
        LosePanel.SetActive(false);
        EnemyDieText = "EnemyDie:" + EnemyDie.ToString() + "/" + Mathf.Round(EnemyNeedToUpLVL).ToString();
        //Health_Text.text = "Health:" + Health;
        TimerText = min.ToString("D2") + " : " + sec.ToString("D2");
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