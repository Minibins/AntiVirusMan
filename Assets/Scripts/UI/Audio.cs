using UnityEngine;
using UnityEngine.UI;


public class Audio : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private float _Volume;
    [SerializeField] private Text MusicVolumeText;
    [SerializeField] private Save save;
    public bool a;

    private void Start()
    {
        _Volume = save.data.MusicVolume;
        MusicVolumeText.text = "Music:" + Mathf.RoundToInt(_Volume * 100) + "%";   
    }

    private void Update()
    {
        _audio.volume = _Volume;
        if (a)
        {
            slider.value = _Volume;
        }
    }

    public void VolumeSlider()
    {
        _Volume = slider.value;
        save.data.MusicVolume = _Volume;
        save.SaveField();
        MusicVolumeText.text = "Music:" + Mathf.RoundToInt(_Volume * 100) + "%";
    }
}
