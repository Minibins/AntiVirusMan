using UnityEngine;
using UnityEngine.UI;


public class Audio : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _Volume;
    [SerializeField] private Save save;
    private float Volume
    {
        get => _Volume; 
        set
            {
                _Volume = value; _audio.volume = Volume;
                slider = Volume;
            }
    }
    private float slider
    {
        get => UiElementsList.instance.Panels.SettingsPanel.MusicVolumeSlider.value;
        set => UiElementsList.instance.Panels.SettingsPanel.MusicVolumeSlider.value = value;
    }

    public void VolumeSlider()
    {
        Volume = slider;
    }
}
