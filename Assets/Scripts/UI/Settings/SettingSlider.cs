using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingSlider : Slider
{
    [SerializeField] public Text text;
    [SerializeField] public string Name, SaveName;

    public string NAME
    {
        set { Name = value; }
    }

    protected override void Start()
    {
        base.Start();

        if (Save.SettingSliders.ContainsKey(SaveName))
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

    public void Startp() => Start();

    private void TextUpdate(float Value)
    {
        text.text = Name + ": " + Convert.ToByte(Value * 100);
        Save.SettingSliders[SaveName] = Value;
        Save.SaveField();
    }
}