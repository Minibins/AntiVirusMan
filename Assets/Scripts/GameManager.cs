using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private int sec;
    [SerializeField] private int min;
    [SerializeField] private int TimeToWin;
    [SerializeField] private bool StopTime = true;
    [SerializeField] private Text LiveTextLose;
    [SerializeField] private SpawnerEnemy SE;
    [SerializeField] private Save save;
    [SerializeField] private Animator anim;
    [SerializeField] private int _level;
    private string _timerText;
    public GameObject SettingsPanel;
    public Action OnTimer { get; set; }

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
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        SettingsPanel.SetActive(false);
        LosePanel.SetActive(false);
        StartCoroutine(TimeFlow());
    }
    public void TakeDamage(float Damage)
    {
        if (Damage > 0)
        {
            return;
        }
        Destroy(Player);
        SE.GetComponent<SpawnerEnemy>().StopOrStartSpawn();
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemy.Length; i++)
        {
            enemy[i].GetComponent<Rigidbody2D>().simulated = false;
            enemy[i].GetComponent<Animator>().SetTrigger("Die");
        }
        LosePanel.SetActive(true);
        StopTime = false;
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
    }
    public void MoveScene(string _Scene)
    {
        SceneManager.LoadScene(_Scene);
    }

    public void OpenSettings(bool Open)
    {
        SettingsPanel.SetActive(Open);
    }
}