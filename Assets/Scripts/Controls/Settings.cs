using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1;
        var SettingsPanel = UiElementsList.instance.Panels.SettingsPanel;
        try
        {
            SettingsPanel.SoundSlider.Startp();
        }
        catch
        {
            UiElementsList.instance = FindObjectOfType<UiElementsList>();
            SettingsPanel.SoundSlider.Startp();
        }

        ChangeUI(!Save.joystick);
        ChangeConsole(Save.console);
        SettingsPanel.Joystick.isOn = !Save.joystick;
        SettingsPanel.Console.isOn = Save.console;
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

    public void ChangeConsole(bool open)
    {
        UiElementsList.instance.Panels.ConsolePanel.SetActive(open);
        Save.console = open;
    }

    public void OpenSettings(bool Open)
    {
        var UI = UiElementsList.instance;
        UI.Panels.SettingsPanel.Panel.SetActive(Open);
        UI.Buttons.All.SetActive(!Open);
        UI.Joysticks.Walk.OnPointerUp(null);
        Time.timeScale = Open ? 0 : 1;
    }

    public void GoToScene(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1;
    }

    public void RestartScene()
    {
        GoToScene(SceneManager.GetActiveScene().name);
    }
}