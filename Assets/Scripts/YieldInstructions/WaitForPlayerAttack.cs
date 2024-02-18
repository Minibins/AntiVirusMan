using DustyStudios;
using System.Collections.Generic;
using UnityEngine;
public class WaitForPlayerAttack : CustomYieldInstruction
{
    bool playerShot = false;
    static List<WaitForPlayerAttack> waitList = new();
    public WaitForPlayerAttack()
    {
        waitList.Add(this);
        playerShot = false;
    }
    public override bool keepWaiting
    {
        get
        {
            if (playerShot)
            {
                playerShot = false;
                waitList.Remove(this);
                return false;
            }
            else return true;
        }
    }
    public void shot() => playerShot=true;
    public static void Shot()
    {
        foreach(WaitForPlayerAttack wait in waitList)
        {
            wait.shot();
        }
        PrecitionWait.DecreaseAllTimers();
    }
}