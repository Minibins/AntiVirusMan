using System;

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

using static UnityEngine.Rendering.DebugUI;

public class SettingSlider : Slider
{
    [SerializeField] public Text text;
    [SerializeField] string Name, SaveName;
    protected override void Start()
    {base.Start();

        if(Save.SettingSliders.ContainsKey(SaveName))
        {
            value = Save.SettingSliders[SaveName];
        }
        else
        {
            value = PlayerPrefs.GetFloat(SaveName);
            Save.SettingSliders.Add(SaveName, value);
        }
        onValueChanged.AddListener(TextUpdate);
        TextUpdate(value);
    }
    private void TextUpdate(float Value) 
    {
        text.text = Name + ": " + Convert.ToByte(Value*100);
        Save.SettingSliders[SaveName] = Value;
        Save.SaveField();
    }
}
