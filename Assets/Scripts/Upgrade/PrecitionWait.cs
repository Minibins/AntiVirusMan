using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrecitionWait : CustomYieldInstruction
{
    private float timer;
    public float Timer
    { get => timer;
        set
        {
            timer = value;
        } 
    }
    static List<PrecitionWait> waitList = new();
    public PrecitionWait(float seconds)
    {
        Timer = seconds;
        waitList.Add(this);
    }
    public override bool keepWaiting
    {
        get
        {
            if(!LevelUP.Items[26].IsTaken)
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
        if(LevelUP.Items[26].IsTaken) 
        { 
            foreach(var item in waitList)
            {
                item.Timer-=1;
            }
        }
    }
}
