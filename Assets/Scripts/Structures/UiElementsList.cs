using System;
using UnityEngine;
using UnityEngine.UI;

public class UiElementsList : MonoBehaviour
{
    public static UiElementsList instance;
    [SerializeField] public ButtonsStruct Buttons;
    [SerializeField] public JoysticksStruct Joysticks;
    [SerializeField] public PanelsStruct Panels;
    [SerializeField] public UserInterface Counters;

    private void Awake()
    {
        instance = this;
    }

    [Serializable]
    public struct ButtonsStruct
    {
        [SerializeField] public Button
            Left, Right, Jump, Attack, Dash, Pique, Settings;

        [SerializeField] public InteractButton Interact;

        public GameObject All
        {
            get => Settings.transform.parent.gameObject;
        }

        public ButtonsStruct(Button left, Button right, Button jump, Button attack, Button dash, Button pique,
            Button settings, InteractButton interact)
        {
            Left = left;
            Right = right;
            Jump = jump;
            Attack = attack;
            Dash = dash;
            Pique = pique;
            Settings = settings;
            Interact = interact;
        }

        public Button[] AsArray
        {
            get =>
                new Button[7]
                {
                    Left,
                    Right,
                    Jump,
                    Attack,
                    Dash,
                    Pique,
                    Settings
                };
        }

        [Serializable]
        public struct InteractButton
        {
            [SerializeField] public Button button;
            [SerializeField] public Image image;
        }
    }

    [Serializable]
    public struct JoysticksStruct
    {
        [SerializeField] public Joystick Attack, Walk;

        public JoysticksStruct(Joystick attack, Joystick walk)
        {
            Attack = attack;
            Walk = walk;
        }
    }

    [Serializable]
    public struct PanelsStruct
    {
        [SerializeField] public GameObject ButtonSettingsPanel, BossPanel, ConsolePanel, SusIPpanel;
        [SerializeField] public RectTransform UpgradesList, Progress;
        [SerializeField] public LoseGamePanel LoseGame;
        [SerializeField] public LevelUp levelUpPanel;
        [SerializeField] public settingsPanel SettingsPanel;
        [SerializeField] public Dialogue DialogueBox;

        public PanelsStruct(settingsPanel SSettingsPanel, GameObject buttonSettingsPanel, GameObject consolePanel,
            LoseGamePanel losePanel, GameObject bossPanel, LevelUp levelUpPanel, Dialogue dialogueBox,
            RectTransform upgradesList, RectTransform progress, GameObject susIPpanel)
        {
            SettingsPanel = SSettingsPanel;
            ButtonSettingsPanel = buttonSettingsPanel;
            ConsolePanel = consolePanel;
            LoseGame = losePanel;
            BossPanel = bossPanel;
            DialogueBox = dialogueBox;
            this.levelUpPanel = levelUpPanel;
            UpgradesList = upgradesList;
            Progress = progress;
            SusIPpanel = susIPpanel;
        }

        [Serializable]
        public struct LevelUp
        {
            [SerializeField] public GameObject Panel, Button1, Button2, Button3;

            public LevelUp(GameObject panel, GameObject button1, GameObject button2, GameObject button3)
            {
                Panel = panel;
                Button1 = button1;
                Button2 = button2;
                Button3 = button3;
            }
        }

        [Serializable]
        public struct settingsPanel
        {
            [SerializeField] public GameObject Panel;
            [SerializeField] public SettingSlider MusicVolumeSlider, SoundSlider;
            [SerializeField] public Toggle Joystick, Console;
        }

        [Serializable]
        public struct LoseGamePanel
        {
            [SerializeField] public GameObject Panel;
            [SerializeField] public Text YouLiveText;
        }

        [Serializable]
        public struct Dialogue
        {
            [SerializeField] public GameObject Panel;
            [SerializeField] public Text text;
        }
    }

    [Serializable]
    public struct UserInterface
    {
        [SerializeField]
        public GameObject All
        {
            get { return Carma.transform.parent.gameObject; }
        }

        [SerializeField] public CellAnimator[] AmmoCell, HealthCell;
        [SerializeField] public Image Time, Lvl, Carma;
    }
}