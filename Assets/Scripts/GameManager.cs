using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Portals;
    [SerializeField] private GameObject[] spawnPointPortals;
    
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject firstAidKit;
    [SerializeField] private GameObject[] spawnPointFirstAidKit;
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private GameObject Buttons;
    [SerializeField] private GameObject[] enemy;
    [SerializeField] public int sec;
    [SerializeField] private int TimeToWin;
    [SerializeField] private bool StopTime = true;
    [SerializeField] private Text LiveTextLose;
    [SerializeField] private SpawnerEnemy SE;
    [SerializeField] private Save save;
    [SerializeField] private int _level;
    [SerializeField] private Image TimeSprite;
    [SerializeField] private float fiilSprite;
    [SerializeField] private GameObject HealthPanel;

    public GameObject SettingsPanel;
    public int min;
    public Action OnTimer { get; set; }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        SettingsPanel.SetActive(false);
        LosePanel.SetActive(false);
        StartCoroutine(TimeFlow());
        Time.timeScale = 1;
    }


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            StartSpawnPortals();
        }
    }

    public void StartSpawnAid()
    {
        StartCoroutine(SpawnFirstAidKit());
    }


   IEnumerator SpawnFirstAidKit()
    {
        while (true)
        {
           GameObject _firstAidKit =     Instantiate(firstAidKit, spawnPointFirstAidKit[UnityEngine.Random.Range(0, spawnPointFirstAidKit.Length)].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(60);
            Destroy(_firstAidKit);
        }
    }


    public void StartSpawnPortals()
    {
        StartCoroutine(SpawnPortals());
    }


 IEnumerator SpawnPortals()
    {
        while (true)
        {
              GameObject Portals1 =  Instantiate(Portals[0], spawnPointPortals[UnityEngine.Random.Range(2, spawnPointPortals.Length)].transform.position, Quaternion.identity);
            
            GameObject Portals2 = Instantiate(Portals[1], spawnPointPortals[UnityEngine.Random.Range(2, spawnPointPortals.Length)].transform.position, Quaternion.identity);
            Portals1.GetComponent<Portals>().secondPortal = Portals2;
            Portals2.GetComponent<Portals>().secondPortal = Portals1;
            yield return new WaitForSeconds(10);
            Destroy(Portals1);
            Destroy(Portals2);
        }
  
    }



    public void LoseGame()
    {
        SE.GetComponent<SpawnerEnemy>().StopOrStartSpawn();
        LosePanel.SetActive(true);
        HealthPanel.SetActive(false);
        Buttons.SetActive(false);
        StopTime = false;
        Antivirus();
    }
    public void Antivirus()
    {   enemy = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < enemy.Length; i++)
        {
            Destroy(enemy[i]);
        }
    }
    IEnumerator TimeFlow()
    {
        while (StopTime == true)
        {
            if (sec == 59)
            {
                min++;
                sec = -1;
                if (min * 60 >= (TimeToWin * 60) + 10)
                {
                    GameObject boss = GameObject.FindGameObjectWithTag("Boss");
                    if (boss = null)
                    {
                        save.LoadField();
                        if (save.data.WinLocation < _level + 1)
                        {
                            save.data.WinLocation = _level + 1;
                            save.SaveField();
                        }
                    }
                }
            }
            fiilSprite = (Convert.ToSingle(min) * 60 + Convert.ToSingle(sec)) / (Convert.ToSingle(TimeToWin) * 60);
            TimeSprite.fillAmount = fiilSprite;
            sec++;
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
        LevelUP.isTaken = null;
    }

    public void OpenSettings(bool Open)
    {
        SettingsPanel.SetActive(Open);
        Buttons.SetActive(!Open);
       Time.timeScale= Open ? 0 : 1;
    }
}