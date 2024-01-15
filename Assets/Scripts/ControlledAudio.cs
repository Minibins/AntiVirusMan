using UnityEngine;

public class ControlledAudio : MonoBehaviour
{
    void Start()
    {
        GetComponent<AudioSource>().volume = UiElementsList.instance.Panels.SettingsPanel.SoundSlider.value;
    }
}
