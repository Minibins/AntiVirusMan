using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;
using UnityEngine.UI;

public class UiElementsList : MonoBehaviour
{
    public static UiElementsList instance;
    [SerializeField] public ButtonsStruct Buttons;
    [SerializeField] public JoysticksStruct Joysticks;
    [SerializeField] public PanelsStruct Panels;
    [SerializeField] public UserInterface Counters;
    private void Start()
    {
        instance = this;
    }
    [System.Serializable]
    public struct ButtonsStruct
    {
        [SerializeField] public Button 
            Left
            , Right
            , Jump
            , Attack
            , Dash
            , Pique
            , Settings;
        [SerializeField] public InteractButton Interact;
        public GameObject All { get => Settings.transform.parent.gameObject; }

        public ButtonsStruct(Button left,Button right,Button jump,Button attack,Button dash,Button pique,Button settings,InteractButton interact)
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
        [System.Serializable]
        public struct InteractButton
        {
            [SerializeField] public Button button;
            [SerializeField] public Image image;
        }
    }
    [System.Serializable]
    public struct JoysticksStruct
    {
        [SerializeField] public Joystick Attack, Walk;
        public JoysticksStruct(Joystick attack,Joystick walk)
        {
            Attack = attack;
            Walk = walk;
        }
    }
    [System.Serializable]
    public struct PanelsStruct 
    {
        [SerializeField] public GameObject ButtonSettingsPanel,BossPanel;
        [SerializeField] public LoseGamePanel LoseGame;
        [SerializeField] public LevelUp levelUpPanel;
        [SerializeField] public settingsPanel SettingsPanel;
        [SerializeField] public Dialogue DialogueBox;
        public PanelsStruct(settingsPanel SSettingsPanel,GameObject buttonSettingsPanel,LoseGamePanel losePanel,GameObject bossPanel,LevelUp levelUpPanel, Dialogue dialogueBox)
        {
            SettingsPanel = SSettingsPanel;
            ButtonSettingsPanel = buttonSettingsPanel;
            LoseGame = losePanel;
            BossPanel = bossPanel;
            DialogueBox = dialogueBox;
            this.levelUpPanel = levelUpPanel;
        }
        [System.Serializable]
        public struct LevelUp
        {
            [SerializeField] public GameObject Panel,Button1,Button2,Button3;

            public LevelUp(GameObject panel,GameObject button1,GameObject button2,GameObject button3)
            {
                Panel = panel;
                Button1 = button1;
                Button2 = button2;
                Button3 = button3;
            }
        }
        [System.Serializable]
        public struct settingsPanel
        {
            [SerializeField] public GameObject Panel;
            [SerializeField] public SettingSlider MusicVolumeSlider, SoundSlider;
        }
        [System.Serializable]
        public struct LoseGamePanel
        {
            [SerializeField] public GameObject Panel;
            [SerializeField] public Text YouLiveText;
        }
        [System.Serializable]
        public struct Dialogue
        {
            [SerializeField] public GameObject Panel;
            [SerializeField] public Text text;
        }
    }
    [System.Serializable]
    public struct UserInterface
    {
        [SerializeField] public GameObject All
        {
            get { return Carma.transform.parent.gameObject; }
        }
        [SerializeField] public CellAnimator[] AmmoCell,HealthCell;
        [SerializeField] public Image Time,Lvl,Carma;
    }
}
