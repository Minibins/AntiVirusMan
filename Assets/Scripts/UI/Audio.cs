using UnityEngine;
using UnityEngine.UI;


public class Audio : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    private float Volume
    {
        set
            {
                _audio.volume = value;
            }
    }
    private SettingSlider slider
    {
        get => UiElementsList.instance.Panels.SettingsPanel.MusicVolumeSlider;
    }
    private void Start()
    {
        try
        {
        slider.Startp();
            Volume = slider.value;
        }
        catch
        {
            UiElementsList.instance = GameObject.FindObjectOfType<UiElementsList>();
            Start();
        }
    }
    public void VolumeSlider()
    {
        Volume = slider.value;
    }
}
