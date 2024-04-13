using DustyStudios;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

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
    [SerializeField] private Image UpgradeIndicator, FixedProgressionImage;
    [SerializeField] private Sprite none, unselectedFixedProgressionLine, selectedFixedProgressionLine;
    public static List<Upgrade> Items = new List<Upgrade>();
    public static List<Upgrade> pickedItems = new List<Upgrade>();

    public static List<FixedProgressionUpgrade> FixedProgressionItems = new List<FixedProgressionUpgrade>();
    public static Dictionary<int, Action> FixedProgressionUpgradeActions = new Dictionary<int, Action>();

    private void Awake()
    {
        FixedProgressionUpgradeActions.Clear();
        UpgradeButton.UpgradeActions.Clear();
        instance = this;
        Items.Clear();
        FixedProgressionItems.Clear();
        pickedItems.Clear();
    }
    public virtual void NewUpgrade()
    {
        
        IEnumerable<int> availableIndexes = Enumerable.Range(0, Items.Count).Where(i => !Items[i].IsTaken).OrderBy(_ => UnityEngine.Random.value).Take(3);

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
        generateButton(first,firstButton);
        generateButton(second,secondButton);
        generateButton(third,thirdButton);
        
        void generateButton(int id,GameObject button)
        {
            button.GetComponent<Image>().sprite = id != -1 ? Items[id].sprite : none;
            button.GetComponent<UpgradeButton>().id = id;
            if(id == Items.Count - 1)
                Instantiate(BuyButton,button.transform);
        }
    }
    public static void AddPickedItem(Upgrade upgrade)
    { 
        pickedItems.Add(upgrade);
        Image image = Instantiate(instance.UpgradeIndicator);
        image.sprite = upgrade.sprite;
        image.transform.SetParent( UiElementsList.instance.Panels.UpgradesList);
    }
    public static void AddFixedProgressionItem(FixedProgressionUpgrade upgrade)
    {
        while(FixedProgressionItems.Count<upgrade.Level)
        {
            FixedProgressionItems.Add(null);
            Image image = Instantiate(instance.UpgradeIndicator);
            image.sprite = instance.unselectedFixedProgressionLine;
            image.transform.SetParent(UiElementsList.instance.Panels.Progress,false);
            if(upgrade != null) upgrade.Image = image;
        }
        FixedProgressionItems[upgrade.Level-1] = upgrade;
        if(!FixedProgressionUpgradeActions.ContainsKey(upgrade.Level))
            FixedProgressionUpgradeActions.Add(upgrade.Level,upgrade.Pick);
        else FixedProgressionUpgradeActions[upgrade.Level] += ()=>upgrade.Pick();
        upgrade.Image = UiElementsList.instance.Panels.Progress.GetChild(upgrade.Level-1).GetComponent<Image>();
        upgrade.Image.sprite = upgrade.notSelectedSprite;
    }

    static public void Select()
    {
        UiElementsList.instance.Panels.levelUpPanel.Panel.SetActive(false);
        Time.timeScale = 1;
        if(FixedProgressionUpgradeActions.ContainsKey(pickedItems.Count)) FixedProgressionUpgradeActions[pickedItems.Count]();
        else if(UiElementsList.instance.Panels.Progress.childCount> pickedItems.Count - 1) UiElementsList.instance.Panels.Progress.GetChild(pickedItems.Count-1).GetComponent<Image>().sprite = instance.selectedFixedProgressionLine;
    }
    [DustyConsoleCommand("getitem","Get item with id", typeof(int))]
    public static string GetItem(int ID)
    {
        UpgradeButton.UpgradeActions[ID]();
        AddPickedItem(Items[ID]);
        Select();
        return "Given item "+ ID;
    }
    [DustyConsoleCommand("itemaction","Get item actions",typeof(int))]
    static string GetUpgradeListeners(int ID)
    {
        var upgradeActions = UpgradeButton.UpgradeActions[ID];

        if(upgradeActions == null || upgradeActions.GetInvocationList().Length == 0)
        {
            return "No actions for Item " + ID;
        }

        var listenerNames = upgradeActions.GetInvocationList()
        .Select(listener => $"{listener.Method.DeclaringType}.{listener.Method.Name}")
        .ToArray();
        return $"Item {ID} has {listenerNames.Length} actions:\n{string.Join("\n",listenerNames)}";
    }
    [DustyConsoleCommand("godmode","Get all ugrades")]
    static string Godmode()
    {
        CoroutineRunner.instance.StartCoroutine(enumerator());
        IEnumerator enumerator()
        {
            for(int i = 0; i < Items.Count;)
            {
                GetItem(i++);
                yield return new WaitForEndOfFrame();
            }
        }
        return "Ok, now you are god";
    }
}