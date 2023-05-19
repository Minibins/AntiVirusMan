using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
    [SerializeField] private int a;
    [SerializeField] private GameObject[] location;
    [SerializeField] private GameObject Perehod;
    [SerializeField] private float TimePerehod;
    private void OnMouseDown()
    {
        StartCoroutine(Change());
    }
    public void PerehodOn()
    {
        Perehod.SetActive(true);
    }

    IEnumerator Change()
    {
        PerehodOn();
        yield return new WaitForSeconds(TimePerehod);
        if (a == 0)
        {
            location[0].SetActive(true);
            location[1].SetActive(false);
            location[2].SetActive(false);
        }
        else if (a == 1)
        {
            location[0].SetActive(false);
            location[1].SetActive(true);
            location[2].SetActive(false);

        }
        else if (a == 2)
        {
            location[0].SetActive(false);
            location[1].SetActive(false);
            location[2].SetActive(true);
        }
    }
}