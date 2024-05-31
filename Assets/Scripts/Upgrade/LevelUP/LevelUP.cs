using DustyStudios;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class LevelUP : MonoBehaviour
{
    static public LevelUP instance;
    [DustyConsoleCommand("panel","Generate upgrade selection panel",typeof(int),typeof(int),typeof(int))]
    public static string Generate(int first,int second,int third)
    {
        instance.generate(first, second, third);
        return "Choose what you like best";
    }
    [DustyConsoleCommand("panel","Generate upgrade selection panel")]
    public static string Generate()
    {
        instance.NewUpgrade();
        return "Choose what you like best";
    }
    private GameObject firstButton => UiElementsList.instance.Panels.levelUpPanel.Button1;
    private GameObject secondButton => UiElementsList.instance.Panels.levelUpPanel.Button2;
    private GameObject thirdButton => UiElementsList.instance.Panels.levelUpPanel.Button3;
    [SerializeField] private GameObject BuyButton;
    [SerializeField] private Image UpgradeIndicator, FixedProgressionImage;
    [SerializeField] private Sprite none, unselectedFixedProgressionLine, selectedFixedProgressionLine;
    public Sprite None { set => none = value; }
    public static List<Upgrade> Items = new List<Upgrade>();
    public static List<Upgrade> pickedItems = new List<Upgrade>();

    public static List<FixedProgressionUpgrade> FixedProgressionItems = new List<FixedProgressionUpgrade>();
    public static Dictionary<int, Action> FixedProgressionUpgradeActions = new Dictionary<int, Action>();

    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        SceneManager.sceneUnloaded += (s) => ClearLists();
    }
    private void ClearLists()
    {
        FixedProgressionUpgradeActions.Clear();
        UpgradeButton.UpgradeActions.Clear();
        Items.Clear();
        FixedProgressionItems.Clear();
        pickedItems.Clear();
        print("All lists cleared");
        SceneManager.sceneUnloaded -= (s) => ClearLists();
    }
    public virtual void NewUpgrade()
    {
        var weightedIndexes = new List<(int index, uint weight)>();
        for(int i = 0; i < Items.Count; i++)
            weightedIndexes.Add((i, Items[i].Weight));
        List<int> indexes = new List<int>();
        for(int i = 0; i < 3; i++)
        {
            if(weightedIndexes.Count == 0)
                break;
            long totalWeight = weightedIndexes.Sum(wi => wi.weight);
            uint randomValue = (uint)UnityEngine.Random.Range(0, totalWeight),cumulativeWeight = 0;
            for(int j = 0; j < weightedIndexes.Count; j++)
            {
                cumulativeWeight += weightedIndexes[j].weight;
                if(randomValue < cumulativeWeight)
                {
                    indexes.Add(weightedIndexes[j].index);
                    weightedIndexes.RemoveAt(j);
                    break;
                }
            }
        }

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
            if(id != -1)
            {
                Upgrade upgrade = Items[id];
                if(upgrade.anotherSprite != null) button.GetComponent<CustomRendererSpriteChanger>().SetSpriteSo(upgrade.anotherSprite);
                else button.GetComponent<CustomRendererSpriteChanger>().SetSprite(upgrade.sprite);
            }
            else button.GetComponent<CustomRendererSpriteChanger>().SetSprite(none);
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