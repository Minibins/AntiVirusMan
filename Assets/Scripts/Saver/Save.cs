using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
public class Save : MonoBehaviour
{
    const string JoystickSaveName = "Joystick";
    const string VersionSaveName = "LastSessionVersion";
    const string LocationSaveName = "WinLocation";
    private static Data _data;

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
            if(value> _data.WinLocation)
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
            SaveField() ;
        }
    }
    public static Dictionary<string,float> SettingSliders
    {
        get { return _data.SettingSliders; }
        set { _data.SettingSliders = value; SaveField(); }
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
    private void Awake()
    {
        LoadField();
    }
    public static void LoadField()
    {
        _data.Set(
            new Dictionary<string,float>(),
            PlayerPrefs.GetInt(LocationSaveName),
            PlayerPrefs.GetString(VersionSaveName),
            Convert.ToBoolean(PlayerPrefs.GetInt(JoystickSaveName))
            );
    }
    public static void SaveField()
    {
        PlayerPrefs.SetInt(LocationSaveName,_data.WinLocation);
        PlayerPrefs.SetString(VersionSaveName,_data.LastSessionVersion);
        PlayerPrefs.SetInt(JoystickSaveName,Convert.ToByte(_data.Joystick));
        foreach(var s in SettingSliders)
        {
            PlayerPrefs.SetFloat(s.Key,s.Value);
        }
        PlayerPrefs.Save();
    }
    public struct Data
    {
        public int WinLocation;
        public string LastSessionVersion;
        public bool Joystick;
        public Dictionary<string, float> SettingSliders;
        public Data(Dictionary<string,float> SettingSliders, int WinLocation,string LastSessionVersion,bool joystick)
        {
            this.SettingSliders = SettingSliders;
            this.WinLocation = WinLocation;
            this.LastSessionVersion = LastSessionVersion;
            Joystick = joystick;
        }
        public void Set(Dictionary<string,float> SettingSliders,int WinLocation,string LastSessionVersion,bool joystick)
        {
            this.SettingSliders = SettingSliders;
            this.WinLocation = WinLocation;
            this.LastSessionVersion = LastSessionVersion;
            Joystick = joystick;
        }
    }

}
