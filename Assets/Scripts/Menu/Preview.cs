using UnityEngine;

public class Preview : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    
    private void AfterDisable()
    {
        MainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
