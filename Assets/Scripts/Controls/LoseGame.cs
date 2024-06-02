using System;
using DustyStudios;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseGame : MonoBehaviour, ISingleton
{
    public static LoseGame instance;
    [SerializeField] private SpawnerEnemy SE;
    [SerializeField] private GameObject HealthPanel;
    [SerializeField] private GameObject Buttons;
    [SerializeField] private GameObject LosePanel;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Lose();
        }
    }

    public void Lose()
    {
        if (SE != null)
        {
            SE.GetComponent<SpawnerEnemy>();
            if (SE.isSpawn) SE.StopOrStartSpawn();
        }

        var UI = UiElementsList.instance;
        var LosePanel = UI.Panels.LoseGame;
        LosePanel.YouLiveText.GetComponent<TimeAnimation>().endTime = Timer.min.ToString() + ":" + Timer.sec.ToString();
        LosePanel.Panel.SetActive(true);
        UI.Counters.All.SetActive(false);
        UI.Buttons.All.SetActive(false);
        Timer.StopTime = false;
        Antivirus();
    }

    [DustyConsoleCommand("sos", "Destroy viruses if 0, Scan viruses if 1, kill viruses if else", typeof(int))]
    public static string Sos(int mode)
    {
        switch (mode)
        {
            case 0:
                Antivirus();
                return "Everyone is cleared";
            case 1:
                foreach (DebuffBank bank in FindObjectsOfType<DebuffBank>()) bank.AddDebuff(new ScannerDebuff());
                Scanner.EndScan();
                return "Everyone is scanned";
            default:
                foreach (EnemyHealth enemy in FindObjectsOfType<EnemyHealth>()) enemy.ApplyDamage(999);
                return "Everyone is killed";
        }
    }

    public static void Antivirus()
    {
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject go in enemy)
            Destroy(go);
    }

    public void MoveScene(string _Scene)
    {
        SceneManager.LoadScene(_Scene);
        LevelUP.Items.Clear();
    }
}