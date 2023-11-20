using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseGame : MonoBehaviour
{
    
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private SpawnerEnemy SE;
    [SerializeField] private GameObject HealthPanel;
    [SerializeField] private GameObject Buttons;
    [SerializeField] private GameObject LosePanel;
    public Timer timer;
    
    public void Lose()
    {
        SE.GetComponent<SpawnerEnemy>().StopOrStartSpawn();
        LosePanel.SetActive(true);
        HealthPanel.SetActive(false);
        Buttons.SetActive(false);
        timer.StopTime = false;
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
