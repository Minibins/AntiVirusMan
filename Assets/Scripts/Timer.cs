using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static int sec;
    public bool StopTime = true;
    public static int min;
    
    [SerializeField] private int _level;
    [SerializeField] private Save save;
    [SerializeField] private int TimeToWin;
    [SerializeField] private float fiilSprite;
    [SerializeField] private Image TimeSprite;
    [SerializeField] private Text LiveTextLose;
    
    
    
    public Action OnTimer { get; set; }
    
    public IEnumerator TimeFlow()
    {
        while (StopTime == true)
        {
            if (sec == 59)
            {
                min++;
                sec = -1;
                if (min * 60 >= (TimeToWin * 60) + 10)
                {
                    GameObject boss = GameObject.FindGameObjectWithTag("Boss");
                    if (boss = null)
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
            LiveTextLose.text = "You live:" + min.ToString("D2") + " : " + sec.ToString("D2");
            yield return new WaitForSeconds(1);
        }
    }
    
    private void OnDisable()
    {
        OnTimer = null;
    }
}
