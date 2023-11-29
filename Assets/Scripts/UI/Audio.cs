using UnityEngine;
using UnityEngine.UI;


public class Audio : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _Volume;
    [SerializeField] private Save save;
    private string MusicVolumeText
    {
        set
        {
            UiElementsList.instance.Panels.SettingsPanel.MusicVolumeText.text = value;
        }
    }
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
        get
        {
            return UiElementsList.instance.Panels.SettingsPanel.MusicVolumeSlider.value;
        }
        set
        {
            UiElementsList.instance.Panels.SettingsPanel.MusicVolumeSlider.value = value;
        }
    }

    private void Start()
    {
        Volume = Save.Volume;
        MusicVolumeText = "Music:" + (Volume * 100) + "%";   
    }

    public void VolumeSlider()
    {
        Volume = slider;
        Save.Volume = Volume;
        Save.SaveField();
        MusicVolumeText = "Music:" + Mathf.RoundToInt(Volume * 100) + "%";
    }
}
