using UnityEngine;

public class PlayerPrefsDependentAnimation : MonoBehaviour
{
    [SerializeField]private string defaultName, prefsKey;
    void Start()
    {
        GetComponent<Animator>().Play(PlayerPrefs.GetString(prefsKey,defaultName));
        Destroy(this);
    }
}
