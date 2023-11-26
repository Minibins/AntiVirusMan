﻿using System;
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
        if (save.data.WinLocation >= _Scene)
        {
            SceneManager.LoadScene(_Scene);
        }
    }

    private void Awake()
    {
        save.LoadField();
        if (save.data.LastSessionVersion != CurrentVersion)
        {
            Changelog.SetActive(true);
        }

        save.data.LastSessionVersion = CurrentVersion;
    }

    private void Update()
    {
        save.SaveField();
        
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
            System.Environment.Exit(0);
    }


    public void ExitGame()
    {
        save.SaveField();

        System.Environment.Exit(0);
        Application.Quit();
    }

    public void OpenWebBrowser()
    {
        Application.OpenURL("https://patreon.com/DustyStudio");
    }
}