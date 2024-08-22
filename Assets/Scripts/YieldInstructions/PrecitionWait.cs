using DustyStudios;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PrecitionWait : CustomYieldInstruction
{
    private float timer;

    public float Timer
    {
        get => timer;
        set { timer = value; }
    }

    private static List<PrecitionWait> waitList = new List<PrecitionWait>();

    public PrecitionWait(float seconds, float shots)
    {
        if (seconds <= 0 || shots <= 0) throw new InvalidOperationException();
        if(LevelUP.Items.Count <= 26) WorkLikeTimer();
            else if(!LevelUP.Items[26].IsTaken) WorkLikeTimer();
        else Timer = shots;
        waitList.Add(this);

        void WorkLikeTimer()
        {
            Timer = seconds;
        }
    }

    public override bool keepWaiting
    {
        get
        {

            if(!LevelUP.IsItemTaken(26))
            {
                Timer -= Time.deltaTime;
            }

            if(timer <= 0)
            {
                waitList.Remove(this);
            }

            return Timer > 0;
        }
    }

    public static void DecreaseAllTimers()
    {
        if (LevelUP.Items[26].IsTaken)
        {
            foreach (var item in waitList)
            {
                item.Timer -= 1;
            }
        }
    }
}