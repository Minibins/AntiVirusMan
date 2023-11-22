using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PChealth : Health
{

    [SerializeField] private HealthCell[] healthCells;
    public override void ApplyDamage(float damage)
    {
        base.ApplyDamage(damage);
        for(int i = 0; i < CurrentHealth; i++)
        {
            healthCells[i].Enable();
        }

        for(int i = _maxHealth - 1; i >= Mathf.Max(CurrentHealth,0); i--)
        {
            healthCells[i].Disable();
        }

    }
    public override void HealHealth(int health)
    {
        base.HealHealth(health);
        for(int i = 0; i < CurrentHealth; i++)
        {
            healthCells[i].Enable();
        }
    }
    public override void DestroyHimself()
    {
        GameObject.FindGameObjectWithTag("LoseGame").GetComponent<LoseGame>().Lose();
        SoundDead();
    }
}
