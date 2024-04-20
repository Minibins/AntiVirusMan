using System;
using System.Collections.Generic;
using DustyStudios;
using UnityEngine;

public class Save : MonoBehaviour
{
    private const string JoystickSaveName = "Joystick";
    private const string ConsoleSaveName = "ConsoleEnable";
    private const string VersionSaveName = "LastSessionVersion";
    private const string LocationSaveName = "WinLocation";
    private static Data _data = new Data();

    public static Data data
    {
        get
        {
            LoadField();
            return _data;
        }
        set
        {
            _data = value;
            SaveField();
        }
    }

    public static int WinLocation
    {
        get
        {
            LoadField();
            return _data.WinLocation;
        }
        set
        {
            if (value > _data.WinLocation)
                _data.WinLocation = value;
            SaveField();
        }
    }

    public static string LastSessionVersion
    {
        get
        {
            LoadField();
            return _data.LastSessionVersion;
        }
        set
        {
            _data.LastSessionVersion = value;
            SaveField();
        }
    }

    public static Dictionary<string, float> SettingSliders
    {
        get { return _data.SettingSliders; }
        set
        {
            _data.SettingSliders = value;
            SaveField();
        }
    }

    public static bool joystick
    {
        get
        {
            LoadField();
            return _data.Joystick;
        }
        set
        {
            _data.Joystick = value;
            SaveField();
        }
    }

    public static bool console
    {
        get
        {
            LoadField();
            return _data.Console;
        }
        set
        {
            DustyConsoleInGame.UsedConsoleInSession |= value;
            _data.Console = value;
            SaveField();
        }
    }

    private void Awake()
    {
        LoadField();
    }

    public static void LoadField()
    {
        _data.Set(
            new Dictionary<string, float>(),
            getInt(LocationSaveName),
            PlayerPrefs.GetString(VersionSaveName),
            Convert.ToBoolean(getInt(JoystickSaveName)),
            Convert.ToBoolean(getInt(ConsoleSaveName))
        );
        int getInt(string key) => PlayerPrefs.GetInt(key);
    }

    public static void SaveField()
    {
        setInt(LocationSaveName, _data.WinLocation);
        setInt(JoystickSaveName, Convert.ToByte(_data.Joystick));
        setInt(ConsoleSaveName, Convert.ToByte(_data.Console));
        PlayerPrefs.SetString(VersionSaveName, _data.LastSessionVersion);
        foreach (var s in SettingSliders)
        {
            PlayerPrefs.SetFloat(s.Key, s.Value);
        }

        PlayerPrefs.Save();
        void setInt(string name, int value) => PlayerPrefs.SetInt(name, value);
    }

    public class Data
    {
        public int WinLocation;
        public string LastSessionVersion;
        public bool Joystick, Console;
        public Dictionary<string, float> SettingSliders;

        public void Set(Dictionary<string, float> SettingSliders, int WinLocation, string LastSessionVersion,
            bool joystick, bool console)
        {
            this.SettingSliders = SettingSliders;
            this.WinLocation = WinLocation;
            this.LastSessionVersion = LastSessionVersion;
            Joystick = joystick;
            Console = console;
        }
    }
}