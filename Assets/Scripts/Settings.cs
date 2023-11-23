using UnityEngine.SceneManagement;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private GameObject Buttons;
    [SerializeField] private GameObject joystick;
    [SerializeField] private GameObject leftButton;
    [SerializeField] private GameObject rightButton;
    private bool isUsingJoystick = true;
    public Timer timer;
    public GameObject SettingsPanel;
    
    private void Start()
    {
        SettingsPanel.SetActive(false);
        LosePanel.SetActive(false);
        timer.StartTimeFlow();
        Time.timeScale = 1;
    }

    public void ChangeUI()
    {
        isUsingJoystick = !isUsingJoystick;

        joystick.SetActive(isUsingJoystick);
        leftButton.SetActive(!isUsingJoystick);
        rightButton.SetActive(!isUsingJoystick);
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
