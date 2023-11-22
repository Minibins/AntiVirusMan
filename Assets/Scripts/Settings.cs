using UnityEngine.SceneManagement;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private GameObject Buttons;
    public Timer timer;
    public GameObject SettingsPanel;
    
    private void Start()
    {
        SettingsPanel.SetActive(false);
        LosePanel.SetActive(false);
        timer.StartTimeFlow();
        Time.timeScale = 1;
    }
    
    public void OpenSettings(bool Open)
    {
        SettingsPanel.SetActive(Open);
        Buttons.SetActive(!Open);
        Time.timeScale = Open ? 0 : 1;
    }
    public void GoToScene(string name) 
    { 
    SceneManager.LoadScene(name);
    }
}
