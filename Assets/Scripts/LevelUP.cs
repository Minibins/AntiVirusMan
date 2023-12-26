using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelUP : MonoBehaviour
{
    private GameObject firstButton=>UiElementsList.instance.Panels.levelUpPanel.Button1;
    private GameObject secondButton => UiElementsList.instance.Panels.levelUpPanel.Button2;
    private GameObject thirdButton => UiElementsList.instance.Panels.levelUpPanel.Button3;
    [SerializeField] private Sprite none;
    public Sprite[] itemtextures;
    [SerializeField] private bool[] IsTakenTemplate;
    public static bool[] isTaken;
    private void Start()
    {
        isTaken = IsTakenTemplate;
    }
    public void NewUpgrade()
    {
        Time.timeScale = 0.1f;
        IEnumerable<int> availableIndexes = Enumerable.Range(0, itemtextures.Length).Where(i => !isTaken[i]).OrderBy(_ => Random.value).Take(3);

        List<int> indexes = availableIndexes.ToList();

        firstButton.GetComponent<Image>().sprite = indexes.Count >= 1 ? itemtextures[indexes[0]] : none;
        secondButton.GetComponent<Image>().sprite = indexes.Count >= 2 ? itemtextures[indexes[1]] : none;
        thirdButton.GetComponent<Image>().sprite = indexes.Count >= 3 ? itemtextures[indexes[2]] : none;

        firstButton.GetComponent<UpgradeButton>().id = indexes.Count >= 1 ? indexes[0] : -1;
        secondButton.GetComponent<UpgradeButton>().id = indexes.Count >= 2 ? indexes[1] : -1;
        thirdButton.GetComponent<UpgradeButton>().id = indexes.Count >= 3 ? indexes[2] : -1;
    }
}