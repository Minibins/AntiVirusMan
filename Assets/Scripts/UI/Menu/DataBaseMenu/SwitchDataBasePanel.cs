using UnityEngine;

public class SwitchDataBasePanel : MonoBehaviour
{
    [SerializeField] private GameObject[] _panels;

    public void Switch(int index)
    {
        foreach (GameObject panel in _panels)
        {
            panel.SetActive(false);
        }
        _panels[index].SetActive(true);
    }
}
