using System;

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

using static UnityEngine.Rendering.DebugUI;

public class SettingSlider : Slider
{
    [SerializeField] public Text text;
    [SerializeField] string Name, SaveName;
    protected override void Awake()
    {base.Awake();

        if(Save.SettingSliders.ContainsKey(SaveName))
        {
            value = Save.SettingSliders[SaveName];
        }
        else
        {
            try
            {
                value = PlayerPrefs.GetFloat(SaveName);
            }
            catch
            {
                PlayerPrefs.SetFloat(SaveName, 1f);
            }
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
