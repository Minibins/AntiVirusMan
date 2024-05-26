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
        if(PlayerPrefs.GetInt("beatenEasterEvent") == 0)
        {
            int collectedEggsCount = 0;
            foreach(var eggState in Save.EggStates.Values)
            {
                if(eggState > 0)
                    collectedEggsCount++;
            }
            if(collectedEggsCount >= 12)
            {
                PlayerPrefs.SetInt("beatenEasterEvent",1);
                PlayerPrefs.Save();
            }
        }
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
        Application.Quit();
    }
}