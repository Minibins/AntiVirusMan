using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private int _level;
    [SerializeField] private int TimeToWin;
    [SerializeField] private float fiilSprite;
    public static int sec;
    public static bool StopTime = true;
    public static int min;
    public Action OnTimer { get; set; }

    private void Start()
    {
        StartCoroutine(TimeFlow());
        fiilSprite = (Convert.ToSingle(min) * 60 + Convert.ToSingle(sec)) / (Convert.ToSingle(TimeToWin) * 60);
    }

    private IEnumerator TimeFlow()
    {
        while (StopTime)
        {
            if (sec == 59)
            {
                min++;
                sec = -1;
                if (min * 60 >= (TimeToWin * 60) + 10)
                {
                    GameObject boss = GameObject.FindGameObjectWithTag("Boss");
                    if (boss == null)
                    {
                        Save.LoadField();
                        if (Save.WinLocation < _level)
                        {
                            Save.WinLocation = _level;
                            Save.SaveField();
                        }
                    }
                }
            }

            fiilSprite = (min * 60 + sec) / (TimeToWin * 60);
            UiElementsList.instance.Counters.Time.fillAmount = fiilSprite;
            sec++;
            yield return new WaitForSeconds(1);
        }
    }
    private void OnDisable()
    {
        OnTimer = null;
    }
}