using UnityEngine;
using System.IO;
using System;
public class Save : MonoBehaviour
{
    const string JoystickSaveName = "Joystick";
    const string VersionSaveName = "LastSessionVersion";
    const string VolumeSaveName = "MusicVolume";
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
            _data.WinLocation = value;
            SaveField();
        }
    }
    public static float Volume
    {
        get
        {
            LoadField();
            return _data.MusicVolume;
        }
        set { _data.MusicVolume = value; SaveField(); }
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
            PlayerPrefs.GetFloat(VolumeSaveName),
            PlayerPrefs.GetInt(LocationSaveName),
            PlayerPrefs.GetString(VersionSaveName),
            Convert.ToBoolean(PlayerPrefs.GetInt(JoystickSaveName))
            ) ;
    }
    public static void SaveField()
    {
        PlayerPrefs.SetFloat(VolumeSaveName,_data.MusicVolume);
        PlayerPrefs.SetInt(LocationSaveName,_data.WinLocation);
        PlayerPrefs.SetString(VersionSaveName,_data.LastSessionVersion);
        PlayerPrefs.SetInt(JoystickSaveName, Convert.ToByte(_data.Joystick));
        PlayerPrefs.Save();
    }
    public struct Data
    {
        public float MusicVolume;
        public int WinLocation;
        public string LastSessionVersion;
        public bool Joystick;
        public Data(float MusicVolume,int WinLocation,string LastSessionVersion,bool joystick)
        {
            this.MusicVolume = MusicVolume;
            this.WinLocation = WinLocation;
            this.LastSessionVersion = LastSessionVersion;
            Joystick = joystick;
        }
        public void Set(float MusicVolume,int WinLocation,string LastSessionVersion,bool joystick)
        {
            this.MusicVolume = MusicVolume;
            this.WinLocation = WinLocation;
            this.LastSessionVersion = LastSessionVersion;
            Joystick = joystick;
        }
    }

}
