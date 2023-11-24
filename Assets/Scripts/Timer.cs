using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private int _level;
    [SerializeField] private Save save;
    [SerializeField] private int TimeToWin;
    [SerializeField] private float fiilSprite;
    [SerializeField] private Image TimeSprite;
    public static int sec;
    public static bool StopTime = true;
    public static int min;
    public Action OnTimer { get; set; }

    private void Start()
    {
        fiilSprite = (Convert.ToSingle(min) * 60 + Convert.ToSingle(sec)) / (Convert.ToSingle(TimeToWin) * 60);
        TimeSprite.fillAmount = fiilSprite;
    }

    public void StartTimeFlow()
    {
        StartCoroutine(TimeFlow());
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
                        save.LoadField();
                        if (save.data.WinLocation < _level)
                        {
                            save.data.WinLocation = _level;
                            save.SaveField();
                        }
                    }
                }
            }

            fiilSprite = (Convert.ToSingle(min) * 60 + Convert.ToSingle(sec)) / (Convert.ToSingle(TimeToWin) * 60);
            TimeSprite.fillAmount = fiilSprite;
            sec++;
            yield return new WaitForSeconds(1);
        }
    }

    private void OnDisable()
    {
        OnTimer = null;
    }
}