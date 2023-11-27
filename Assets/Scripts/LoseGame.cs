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
    [SerializeField] private Text LiveTextLose;
    private void Start()
    {
        instance = this;
    }
    public void Lose()
    {
        SE.GetComponent<SpawnerEnemy>().StopOrStartSpawn();
      //  LiveTextLose.text = "You live:" + Timer.min.ToString("D2") + " : " + Timer.sec.ToString("D2");
      LiveTextLose.text = Timer.min.ToString() + ":" + Timer.sec.ToString();
        LosePanel.SetActive(true);
        HealthPanel.SetActive(false);
        Buttons.SetActive(false);
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
