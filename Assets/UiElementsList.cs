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
        public GameObject All { get => Settings.transform.parent.gameObject; }

        public ButtonsStruct(Button left,Button right,Button jump,Button attack,Button dash,Button pique,Button settings)
        {
            Left = left;
            Right = right;
            Jump = jump;
            Attack = attack;
            Dash = dash;
            Pique = pique;
            Settings = settings;
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
        [SerializeField] public GameObject ButtonSettingsPanel,LosePanel,BossPanel;
        [SerializeField] public LevelUp levelUpPanel;
        [SerializeField] public settingsPanel SettingsPanel;
        public PanelsStruct(settingsPanel SSettingsPanel,GameObject buttonSettingsPanel,GameObject losePanel,GameObject bossPanel,LevelUp levelUpPanel)
        {
            SettingsPanel = SSettingsPanel;
            ButtonSettingsPanel = buttonSettingsPanel;
            LosePanel = losePanel;
            BossPanel = bossPanel;
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
            [SerializeField] public Text MusicVolumeText;
            [SerializeField] public Slider MusicVolumeSlider;
        }
    }
    [System.Serializable]
    public struct UserInterface
    {
        [SerializeField] public CellAnimator[] AmmoCell,HealthCell;
        [SerializeField] public Image Time,Lvl,Carma;
    }
}
