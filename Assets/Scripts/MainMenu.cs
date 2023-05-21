using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] location;
    [SerializeField] private GameObject Perehod;
    public int a;
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

    public void Location(int location)
    {
        a = location;
        StartCoroutine(Change());
    }
    public void PerehodOn()
    {
        Perehod.SetActive(true);
    }


    IEnumerator Change()
    {
        PerehodOn();
        yield return new WaitForSeconds(0.5f);
        if (a == 0)
        {
            location[1].SetActive(false);
            location[2].SetActive(false);
            location[0].SetActive(true);
        }
        else if (a == 1)
        {
            location[1].SetActive(true);
            location[2].SetActive(false);
            location[0].SetActive(false);

        }
        else if (a == 2)
        {
            location[1].SetActive(false);
            location[2].SetActive(true);
            location[0].SetActive(false);
        }
    }




    public void ExitGame()
    {
        save.SaveField();
        Application.Quit();
    }
}
