using UnityEngine;

public class DataBaseMenu : MonoBehaviour
{
  [SerializeField] private GameObject[] _panels;


  public void OpenPanel(int index)
  {
    foreach (GameObject panel in _panels)
    {
      panel.SetActive(false);
    }
    _panels[index].SetActive(true);
  }
}


