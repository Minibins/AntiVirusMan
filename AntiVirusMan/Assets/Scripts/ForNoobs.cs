using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForNoobs : MonoBehaviour
{
    [SerializeField] private GameObject LeftButton;
    [SerializeField] private GameObject RightButton;
    [SerializeField] private GameObject JumpButton;
    [SerializeField] private GameObject ShootButton;
    [SerializeField] private int numTrigger;
    [SerializeField] private float time;

    private void Start()
    {
        StartCoroutine(StartB());
    }

    IEnumerator StartB()
    {
        yield return new WaitForSeconds(time);
        LeftButton.SetActive(true);
        RightButton.SetActive(true);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (numTrigger == 0)
        {
            JumpButton.SetActive(true);
        }

        if (numTrigger == 1)
        {
            ShootButton.SetActive(true);
        }
        if (numTrigger == 2)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
