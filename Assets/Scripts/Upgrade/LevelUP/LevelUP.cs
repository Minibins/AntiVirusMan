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
    //                              Удобные указатели на общую базу данных UiElementsList
    private GameObject firstButton => UiElementsList.instance.Panels.levelUpPanel.Button1;
    private GameObject secondButton => UiElementsList.instance.Panels.levelUpPanel.Button2;
    private GameObject thirdButton => UiElementsList.instance.Panels.levelUpPanel.Button3;

    //                              Новые прокачки имеют кнопку "купить", ведущую на статью в Вики игры
    [SerializeField] private GameObject BuyButton;
    //                              Префабы изображений для индикации фиксированной прогрессии
    [SerializeField] private Image UpgradeIndicator, 
                                   FixedProgressionImage;
    //                              Спрайты шкалы фиксированной прогрессии
    [SerializeField] private Sprite unselectedFixedProgressionLine,
                                    selectedFixedProgressionLine;
    public static List<Upgrade> Items = new List<Upgrade>(),
                                pickedItems = new List<Upgrade>();
    //                              Прокачки фиксированной прогрессии
    public static List<FixedProgressionUpgrade> FixedProgressionItems = new List<FixedProgressionUpgrade>();
    //                              Действия при получении предметов фиксированной прогрессии
    public static Dictionary<int, Action> FixedProgressionUpgradeActions = new Dictionary<int, Action>();

    private void Awake()
    {
        // Чистим списки
        FixedProgressionUpgradeActions.Clear();
        FixedProgressionItems.Clear();
        UpgradeButton.UpgradeActions.Clear();
        pickedItems.Clear();
        Items.Clear();
        // Синглтон
        instance = this;
    }
    public static LevelUP instance;
    public virtual void NewUpgrade()
    {
        //                                  Три случайных ID в списке от 0 до количества прокачек, где прокачки с этим ID ещё не собраны
        int[] indexes = Enumerable.Range(0, Items.Count).Where(i => !Items[i].IsTaken).OrderBy(_ => UnityEngine.Random.value).Take(3).ToArray();
        generate(
            // Если массив ID меньше трёх, посылается -1 
            indexes.Length >= 1 ? indexes[0] : -1,
            indexes.Length >= 2 ? indexes[1] : -1,
            indexes.Length >= 3 ? indexes[2] : -1);
    }
    // Статический метод генерации прокачек по ID
    public static void Generate(int first,int second,int third) => instance.generate(first, second, third);
    public void generate(int first,int second,int third)
    {
#if UNITY_STANDALONE_WIN
        //Зарезервировано под ПК порт.
#endif
        // Активация панели прокачки
        UiElementsList.instance.Panels.levelUpPanel.Panel.SetActive(true);
        Time.timeScale = 0.1f;

        // Генерация кнопок
        generateButton(first,firstButton);
        generateButton(second,secondButton);
        generateButton(third,thirdButton);

        void generateButton(int id,GameObject button)
        {
            button.GetComponent<UpgradeButton>().id = id;
            if(id == Items.Count - 1)
                Instantiate(BuyButton,button.transform);
            if(id == -1) // У прокачек -1 нет спрайта
                return;
            Upgrade upgrade = Items[id];
            if(upgrade.anotherSprite != null) // CustomRendererSpriteChanger - это компонент, заменяющий спрайт спрайтом с поведением.
                                              // Такие спрайты могут содержать коллайдеры, звуки и прочие необычные эффекты.
                    button.GetComponent<CustomRendererSpriteChanger>().SetSpriteSo(upgrade.anotherSprite);
            else button.GetComponent<CustomRendererSpriteChanger>().SetSprite(upgrade.sprite);
        }
    }

    // Этот метод добавляет спрайт прокачки в инвентарь игрока
    public static void AddPickedItem(Upgrade upgrade)
    { 
        pickedItems.Add(upgrade);
        Image image = Instantiate(instance.UpgradeIndicator); 
        image.sprite = upgrade.sprite;
        image.transform.SetParent(UiElementsList.instance.Panels.UpgradesList);
    }

    // Добавляет прокачку фиксированной прогрессии
    public static void AddFixedProgressionItem(FixedProgressionUpgrade upgrade)
    {
        while(FixedProgressionItems.Count<upgrade.Level)
        {
            FixedProgressionItems.Add(null);
            Image image = Instantiate(instance.UpgradeIndicator);
            image.sprite = instance.unselectedFixedProgressionLine;
            image.transform.SetParent(UiElementsList.instance.Panels.Progress,false);
        }
        FixedProgressionItems[upgrade.Level-1] = upgrade;
        if(FixedProgressionUpgradeActions.ContainsKey(upgrade.Level)) //Если уже есть прокачки с действием на этот уровень, прибавляем действие прокачки
            FixedProgressionUpgradeActions[upgrade.Level] += ()=>upgrade.Pick();
        else 
            FixedProgressionUpgradeActions.Add(upgrade.Level,upgrade.Pick);
        upgrade.Image = UiElementsList.instance.Panels.Progress.GetChild(upgrade.Level-1).GetComponent<Image>();
        upgrade.Image.sprite = upgrade.notSelectedSprite;
    }
    // Я в курсе, что в скрипте уже есть почти такой же код, я переделаю эту часть, когда вернусь домой ('_')
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
            return "No actions for Item " + ID;

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
            for(int i = 0; i < Items.Count;i++)
            {
                GetItem(i);
                yield return new WaitForEndOfFrame();
            }
        }
        return "Ok, now you are god";
    }
}