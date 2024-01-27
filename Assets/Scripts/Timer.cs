using System.Reflection;
using System.Collections;
using System;
using Unity.Mathematics;
using DustyStudios;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private int _level;
    [SerializeField] private int TimeToWin;
    [SerializeField] private float fiilSprite;
    public static int sec;
    public static int min;
    public static int time { get => (min * 60 + sec); }
    public static bool StopTime = true;

    private void Start()
    {
        sec = 0;
        min = 0;
        try
        {
            UiElementsList.instance.Counters.Time.name = "time";
        }
        catch
        {
            UiElementsList.instance = FindObjectOfType<UiElementsList>();
        }
        StartCoroutine(TimeFlow());
        fiilSprite = time / TimeToWin * 60;
    }

    private IEnumerator TimeFlow()
    {
        while(StopTime)
        {
            if(sec == 59)
            {
                min++;
                sec = -1;
                if(min * 60 >= (TimeToWin * 60) + 10)
                {
                    GameObject boss = GameObject.FindGameObjectWithTag("Boss");
                    if(boss == null)
                    {
                        Save.LoadField();
                        if(Save.WinLocation < _level)
                        {
                            Save.WinLocation = _level;
                            Save.SaveField();
                        }
                    }
                }
            }
            fiilSprite = math.max(math.min((float)time / ((float)TimeToWin * 60),1f),0f);
            UiElementsList.instance.Counters.Time.fillAmount = fiilSprite;
            sec++;
            yield return new WaitForSeconds(1);
        }
    }

    [DustyConsoleCommand("setsec","Set time in seconds",typeof(int))]
    public static void SetSec(int s)
    {
        sec = s;
        Debug.Log("Time setted: " + time);
    }
    [DustyConsoleCommand("setmin","Set time in minutes",typeof(int))]
    public static void SetMin(int m)
    {
        min = m;
        Debug.Log("Time setted: " + time);
    }
}