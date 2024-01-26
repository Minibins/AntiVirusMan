using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Settings : MonoBehaviour
{   
    private void Start()
    {
        Time.timeScale = 1;

        UiElementsList.instance.Panels.SettingsPanel.SoundSlider.Startp();
    }

    public void ChangeUI(bool isUsingJoystick)
    {
        var UI = UiElementsList.instance;
        isUsingJoystick = !isUsingJoystick;

        UI.Joysticks.Walk.gameObject.SetActive(!isUsingJoystick);
        var Buttons = UI.Buttons;
        Buttons.Right.gameObject.SetActive(isUsingJoystick);
        Buttons.Left.gameObject.SetActive(isUsingJoystick);
        Save.joystick = isUsingJoystick;
    }
       
    public void OpenSettings(bool Open)
    {
        var UI = UiElementsList.instance;
        UI.Panels.SettingsPanel.Panel.SetActive(Open);
        UI.Buttons.All.SetActive(!Open);
        Time.timeScale = Open ? 0 : 1;
    }
    public void GoToScene(string name) 
    { 
    SceneManager.LoadScene(name);
    }
}
