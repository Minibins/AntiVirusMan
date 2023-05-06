using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    // [SerializeField] private GameObject LevelUp;
    [SerializeField] private GameObject LosePanel;
    [SerializeField] public GameObject SettingsPanel;
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private GameObject LevelUp;
    [SerializeField] private int Level;
    //[SerializeField] private float Health;
    [SerializeField] private int sec;
    [SerializeField] private int min;
    [SerializeField] private int TimeToWin;
    [SerializeField] private float EnemyNeedToUpLVL;
    [SerializeField] private float EnemyDie;
    [SerializeField] private bool StopTime = true;
    [SerializeField] private Text EnemyDieText;
    //[SerializeField] private Text Health_Text;
    [SerializeField] private Text TimerText;
    [SerializeField] private Text LiveTextLose;
    [SerializeField] private SpawnerEnemy SE;
    [SerializeField] private Save save;
    [SerializeField] private Animator anim;
    [SerializeField] private int _level;
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

    public void TakeEXP(int kills)
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
            TimerText.text = min.ToString("D2") + " : " + sec.ToString("D2");
            LiveTextLose.text = "You live:" + min.ToString("D2") + " : " + sec.ToString("D2");
            yield return new WaitForSeconds(1);
        }
    }
    public void MoveScene(string _Scene)
    {
        SceneManager.LoadScene(_Scene);
    }

    public void OpenSettings(bool Open)
    {
        SettingsPanel.SetActive(Open);
        TimeFlow();
    }
    private void Settings()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        SettingsPanel.SetActive(false);
        LosePanel.SetActive(false);
        EnemyDieText.text = "EnemyDie:" + EnemyDie.ToString() + "/" + Mathf.Round(EnemyNeedToUpLVL).ToString();
        //Health_Text.text = "Health:" + Health;
        TimerText.text = min.ToString("D2") + " : " + sec.ToString("D2");
    }

    private void Upgrade()
    {
        EnemyDieText.text = "EnemyDie:" + EnemyDie.ToString() + "/" + Mathf.Round(EnemyNeedToUpLVL).ToString();
        if (EnemyDie >= EnemyNeedToUpLVL)
        {
            EnemyDie = 0;
            EnemyNeedToUpLVL *= 1.1f;
            Level++;
            LevelUp.SetActive(true);
            LevelUp.GetComponent<LevelUP>().NewUpgrade();
        }
    }
}