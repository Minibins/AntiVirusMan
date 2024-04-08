using System;

using UnityEngine;
public class PCAcidContact : MonoBehaviour, IAcidable
{
    PC PC;
    RechargingValue Opyanenie = new(new(3,0),0,15,-1,value=> new PrecitionWait(value, 1));
    int Pyanstvo = 0, LastOpyanenieValue=0;
    public void OblitCislotoy()
    {
        Opyanenie++;
    }
    void PyanstvoUpdate(float f)
    {
        PC.Health.multiplerDamage.baseValue = 1+Convert.ToInt16(Opyanenie!=0);
        if(Opyanenie == 0)
            Pyanstvo = 0;
        else 
        {
            Pyanstvo++;
            if(LastOpyanenieValue <= Opyanenie)
                PC.Health.ApplyDamage(Mathf.Clamp(Pyanstvo - 2,0,3));
        }
        print($"Opyanenie = {Opyanenie}, Pyanstvo = {Pyanstvo}");
        PC.Animator.SetInteger("PoisonLevel",Pyanstvo>=3?3:Opyanenie);
        LastOpyanenieValue = Opyanenie;
    }

    void Awake()
    {
        PC = GetComponentInChildren<PC>();
        Opyanenie.ValueChanged += PyanstvoUpdate;
    }
    
}