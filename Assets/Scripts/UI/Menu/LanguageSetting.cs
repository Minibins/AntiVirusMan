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
            int language = (PlayerPrefs.GetInt(Key)+1)%LocalizationSettings.AvailableLocales.Locales.Count;
            PlayerPrefs.SetInt(Key,language);
            try
            {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[language];
            }
            catch {
                print("Your language ID is incorrect:" + language);
                PlayerPrefs.SetInt(Key,0);
            }
            PlayerPrefs.Save();
        }
    }
}