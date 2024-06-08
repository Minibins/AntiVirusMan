using System;
using UnityEngine;

public class PCAcidContact : MonoBehaviour, IAcidable
{
    private PC PC;
    private RechargingValue Opyanenie = new RechargingValue(new (3, 0), 0, 15, -1,value=> new PrecitionWait(value, 1));
    private int Pyanstvo = 0, LastOpyanenieValue = 0;

    public void OblitCislotoy()
    {
        Opyanenie++;
    }

    private void PyanstvoUpdate(float f)
    {
        PChealth.instance.multiplerDamage.BaseValue = 1 + Convert.ToInt16(Opyanenie != 0);
        PC.LowChargeDamage = Opyanenie == 0;
        if (Opyanenie == 0)
            Pyanstvo = 0;
        else
        {
            Pyanstvo++;
            if (LastOpyanenieValue <= Opyanenie)
                PChealth.instance.ApplyDamage(Mathf.Clamp(Pyanstvo - 2, 0, 3));
        }

        PC.Animator.SetInteger("PoisonLevel", Pyanstvo >= 3 ? 3 : Opyanenie);
        LastOpyanenieValue = Opyanenie;
    }

    private void Awake()
    {
        PC = GetComponentInChildren<PC>();
        Opyanenie.ValueChanged += PyanstvoUpdate;
    }
}