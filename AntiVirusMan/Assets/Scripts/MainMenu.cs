using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] location;
    public Save save;

    public void LocationMove(int _Scene)
    {
        if (save.data.WinLocation >= _Scene)
        {
            SceneManager.LoadScene(_Scene + 1);
        }
    }

    private void Start()
    {
        save.LoadField();
    }

    private void Update()
    {
        save.SaveField();
    }

    public void ToLocation()
    {
        location[1].SetActive(false);
        location[2].SetActive(true);
        location[0].SetActive(false);
    }

    public void ExitGame()
    {
        save.SaveField();
        Application.Quit();
    }
}
