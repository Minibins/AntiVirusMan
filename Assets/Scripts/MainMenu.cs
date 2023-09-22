using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public Save save;

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
    }

    private void Update()
    {
        save.SaveField();
    }


    public void ExitGame()
    {
        save.SaveField();
        Application.Quit();
    }
}
