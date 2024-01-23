using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.Android.LowLevel;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Save save;

    [SerializeField]private string CurrentVersion;
    [SerializeField]private GameObject Changelog;
    [SerializeField]string[] scenes;

    public static MainMenu instance;
    public void LocationMove(int id)
    {
        if (Save.data.WinLocation >= id)
        {
            SceneManager.LoadScene(scenes[id]);
        }
    }

    private void Awake()
    {
        instance = this;
        Save.LoadField();
        if (Save.data.LastSessionVersion != CurrentVersion)
        {
            Changelog.SetActive(true);
        }

        Save.LastSessionVersion = CurrentVersion;
    }

    private void Update()
    {
        Save.SaveField();
        
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
            
        
    }


    public void ExitGame()
    {
        Save.SaveField();

        System.Environment.Exit(0);
        Application.Quit();
    }

    public void OpenWebBrowser()
    {
        Application.OpenURL("https://patreon.com/DustyStudio");
    }
}