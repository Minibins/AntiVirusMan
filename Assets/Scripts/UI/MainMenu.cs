using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Save save;
    [SerializeField] private string CurrentVersion;
    [SerializeField] private GameObject Changelog;

    public void LocationMove(int _Scene)
    {
        if (Save.data.WinLocation >= _Scene)
        {
            SceneManager.LoadScene(_Scene);
        }
    }

    private void Awake()
    {
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
            System.Environment.Exit(0);
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