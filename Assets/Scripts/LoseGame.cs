using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseGame : MonoBehaviour
{
    public static LoseGame instance;
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private SpawnerEnemy SE;
    [SerializeField] private GameObject HealthPanel;
    [SerializeField] private GameObject Buttons;
    [SerializeField] private GameObject LosePanel;
    private void Start()
    {
        instance = this;
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
        LosePanel.YouLiveText.text = Timer.min.ToString() + ":" + Timer.sec.ToString();
        LosePanel.Panel.SetActive(true);
        UI.Counters.All.SetActive(false);
        UI.Buttons.All.SetActive(false);
        Timer.StopTime = false;
        Antivirus();
    }

    public void Antivirus()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemy.Length; i++)
        {
            Destroy(enemy[i]);
        }
    }

    public void MoveScene(string _Scene)
    {
        SceneManager.LoadScene(_Scene);
        LevelUP.isTaken = null;
    }
}
