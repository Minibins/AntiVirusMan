using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string CurrentVersion;
    [SerializeField] private GameObject Changelog;
    [SerializeField] string[] scenes;

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

        Environment.Exit(0);
        Application.Quit();
    }
}