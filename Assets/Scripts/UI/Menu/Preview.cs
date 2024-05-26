using UnityEngine;

public class Preview : MonoBehaviour
{
    static bool watchedIntro = false;
    [SerializeField] private GameObject MainMenu;

    private void Awake()
    {
        MainMenu.SetActive(watchedIntro);
    }

    public void AfterDisable()
    {
        watchedIntro = true;
        MainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}