using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] location;
    [SerializeField] private Mover mover;
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
        StartCoroutine(Change());
    }

    public void ExitGame()
    {
        save.SaveField();
        Application.Quit();
    }
    IEnumerator Change()
    {
        mover.PerehodOn();
        yield return new WaitForSeconds(0.4f);
        location[1].SetActive(false);
        location[2].SetActive(true);
        location[0].SetActive(false);
    }
}
