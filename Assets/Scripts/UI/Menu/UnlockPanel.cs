using UnityEngine;

public class UnlockPanel : MonoBehaviour
{
    private void Awake()
    {
        bool isUnlockedSomething = false;
        if(PlayerPrefs.GetInt("beatenEasterEvent") == 0)
        {
            int collectedEggsCount = 0;
            foreach(var eggState in Save.EggStates.Values)
            {
                if(eggState > 0)
                    collectedEggsCount++;
            }
            if(collectedEggsCount >= 12)
            {
                PlayerPrefs.Save();
                isUnlockedSomething = true;
                PlayerPrefs.SetInt("beatenEasterEvent",1);
            }
        }
        gameObject.SetActive(isUnlockedSomething);
    }
}
