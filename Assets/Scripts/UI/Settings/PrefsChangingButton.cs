using UnityEngine;
using UnityEngine.UI;
public class PrefsChangingButton : Button
{
    [SerializeField] private new string name;
    [SerializeField] private string value;
    protected override void Start()
    {
        base.Start();
        onClick.AddListener(()=>SetPrefsString(name,value));
    }
    public void SetPrefsString(string name,string value)=>
        PlayerPrefs.SetString(name,value);
}