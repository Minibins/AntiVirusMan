using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageSetting : MonoBehaviour
{
    private const string Key = "Language";
    void Start()
    {
        PlayerPrefs.SetInt(Key,PlayerPrefs.GetInt(Key) - 1);
        setLanguage();
    }

    public void setLanguage()
    {
        StartCoroutine(enumerator());
        IEnumerator enumerator()
        {
            yield return LocalizationSettings.InitializationOperation;
            int language = (PlayerPrefs.GetInt(Key)+1)%3;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[language];
            PlayerPrefs.SetInt(Key,language);
        }
    }
}