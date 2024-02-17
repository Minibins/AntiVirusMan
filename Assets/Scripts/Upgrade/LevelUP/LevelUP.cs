using DustyStudios;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;
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
        generateButton(first,firstButton);
        generateButton(second,secondButton);
        generateButton(third,thirdButton);
        
        void generateButton(int id,GameObject button)
        {
            button.GetComponent<Image>().sprite = id != -1 ? Items[id].Sprite : none;
            button.GetComponent<UpgradeButton>().id = id;
            if(id == Items.Count - 2)
                Instantiate(BuyButton,button.transform);
        }
    }
    static public void Select()
    {
        UiElementsList.instance.Panels.levelUpPanel.Panel.SetActive(false);
        Time.timeScale = 1;
    }
    [DustyConsoleCommand("getitem","Get item with id", typeof(int))]
    static string GetItem(int ID)
    {
        UpgradeButton.UpgradeActions[ID]();
        LevelUP.Items[ID].IsTaken = true;
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
}