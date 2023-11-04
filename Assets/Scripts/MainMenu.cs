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
            SceneManager.LoadScene(_Scene + 1);
        }
    }

    private void Awake()
    {
        save.LoadField();
        if(save.data.LastSessionVersion != CurrentVersion)
        {
            Changelog.SetActive(true);
        }
        save.data.LastSessionVersion = CurrentVersion;
    }

    private void Update()
    {
        save.SaveField();
    }


    public void ExitGame()
    {
        save.SaveField();
        Debug.Log("Я вышел");
        Application.Quit();
    }
    public void OpenWebBrowser()
    {
        Application.OpenURL("https://patreon.com/DustyStudio");
    }
}
