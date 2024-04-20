using UnityEngine;

public class ControlledAudio : MonoBehaviour
{
    private void Start()
    {
        GetComponent<AudioSource>().volume = UiElementsList.instance.Panels.SettingsPanel.SoundSlider.value;
    }
}