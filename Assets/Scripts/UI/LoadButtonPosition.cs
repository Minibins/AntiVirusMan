using UnityEngine;

public class LoadButtonPosition: MonoBehaviour
{
    [SerializeField] private string playerPrefsKey;
    private void Start()
    {
        if (PlayerPrefs.HasKey(playerPrefsKey))
        {
            Vector3 savedPosition = StringToVector3(PlayerPrefs.GetString(playerPrefsKey));
            transform.position = savedPosition;
        }
    }
    private Vector3 StringToVector3(string stringValue)
    {
        string[] components = stringValue.Split(':');
        float x = float.Parse(components[0]);
        float y = float.Parse(components[1]);
        float z = 0;
        return new Vector3(x, y, z);
    }
}