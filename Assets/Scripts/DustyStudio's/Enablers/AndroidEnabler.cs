using UnityEngine;

public class AndroidEnabler : MonoBehaviour
{
    [SerializeField] GameObject[] gameObjectsToEnableInAndroid;

    private void Start()
    {
#if UNITY_ANDROID
        foreach (var obj in gameObjectsToEnableInAndroid)
            obj.SetActive(true);
#endif
    }
}