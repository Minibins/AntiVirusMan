using UnityEngine;
public class AndroidEnabler : MonoBehaviour
{
    [SerializeField] GameObject[] gameObjectsToEnableInAndroid; 
    void Start()
    {
#if UNITY_ANDROID
        foreach(var obj in gameObjectsToEnableInAndroid)
            if(!obj.active) obj.SetActive(true);
#endif
    }
}
