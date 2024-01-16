using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelUP : MonoBehaviour
{
    static LevelUP instance;
    public static void Generate(int first,int second,int third)
    {
        instance.generate(first, second, third);
    }
    private GameObject firstButton => UiElementsList.instance.Panels.levelUpPanel.Button1;
    private GameObject secondButton => UiElementsList.instance.Panels.levelUpPanel.Button2;
    private GameObject thirdButton => UiElementsList.instance.Panels.levelUpPanel.Button3;

    [SerializeField] private Sprite none;
    public Sprite[] itemtextures;
    [SerializeField] private bool[] IsTakenTemplate;
    public static bool[] isTaken;
    private void Awake()
    {
        UpgradeButton.UpgradeActions.Clear();
        instance = this;
        isTaken = IsTakenTemplate;
    }
    public virtual void NewUpgrade()
    {
        
        IEnumerable<int> availableIndexes = Enumerable.Range(0, itemtextures.Length).Where(i => !isTaken[i]).OrderBy(_ => Random.value).Take(3);

        List<int> indexes = availableIndexes.ToList();
        generate(
            indexes.Count >= 1 ? indexes[0] : -1,
            indexes.Count >= 2 ? indexes[1] : -1,
            indexes.Count >= 3 ? indexes[2] : -1);
    }
    public void generate(int first,int second,int third)
    {
        Time.timeScale = 0.1f;
        firstButton.GetComponent<Image>().sprite = first!=-1 ? itemtextures[first] : none;
        secondButton.GetComponent<Image>().sprite = second != -1 ? itemtextures[second] : none;
        thirdButton.GetComponent<Image>().sprite = third != -1 ? itemtextures[third] : none;

        firstButton.GetComponent<UpgradeButton>().id = first;
        secondButton.GetComponent<UpgradeButton>().id = second;
        thirdButton.GetComponent<UpgradeButton>().id = third;
    }
}