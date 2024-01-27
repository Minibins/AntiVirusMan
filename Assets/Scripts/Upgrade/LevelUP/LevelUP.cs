using DustyStudios;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelUP : MonoBehaviour
{

    static public LevelUP instance;
    public static void Generate(int first,int second,int third)
    {
        instance.generate(first, second, third);
    }
    private GameObject firstButton => UiElementsList.instance.Panels.levelUpPanel.Button1;
    private GameObject secondButton => UiElementsList.instance.Panels.levelUpPanel.Button2;
    private GameObject thirdButton => UiElementsList.instance.Panels.levelUpPanel.Button3;
    [SerializeField] private GameObject BuyButton;

    [SerializeField] private Sprite none;
    public static List<Upgrade> Items = new List<Upgrade>();
    private void Awake()
    {
        UpgradeButton.UpgradeActions.Clear();
        instance = this;
        Items.Clear();
    }
    public virtual void NewUpgrade()
    {
        
        IEnumerable<int> availableIndexes = Enumerable.Range(0, Items.Count).Where(i => !Items[i].IsTaken).OrderBy(_ => Random.value).Take(3);

        List<int> indexes = availableIndexes.ToList();
        generate(
            indexes.Count >= 1 ? indexes[0] : -1,
            indexes.Count >= 2 ? indexes[1] : -1,
            indexes.Count >= 3 ? indexes[2] : -1);
    }
    public void generate(int first,int second,int third)
    {
#if UNITY_STANDALONE_WIN

 

#endif
        
        UiElementsList.instance.Panels.levelUpPanel.Panel.SetActive(true);
        Time.timeScale = 0.1f;
        firstButton.GetComponent<Image>().sprite = first!=-1 ? Items[first].Sprite : none;
        secondButton.GetComponent<Image>().sprite = second != -1 ? Items[second].Sprite : none;
        thirdButton.GetComponent<Image>().sprite = third != -1 ? Items[third].Sprite : none;

        firstButton.GetComponent<UpgradeButton>().id = first;
        secondButton.GetComponent<UpgradeButton>().id = second;
        thirdButton.GetComponent<UpgradeButton>().id = third;

             if(first == Items.Count-1) Instantiate(BuyButton,firstButton. transform);
        else if(second== Items.Count-1) Instantiate(BuyButton,secondButton.transform);
        else if(third == Items.Count-1) Instantiate(BuyButton,thirdButton. transform);
    }
    static public void Select()
    {
        UiElementsList.instance.Panels.levelUpPanel.Panel.SetActive(false);
        Time.timeScale = 1;
    }
    [DustyConsoleCommand("getitem","Get item with id", typeof(int))]
    static void GetItem(int ID)
    {
        UpgradeButton.UpgradeActions[ID]();
        LevelUP.Items[ID].IsTaken = true;
    }
}