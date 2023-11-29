using UnityEngine;

public class PChealth : DefaultHealth
{

    
    public override void ApplyDamage(float damage)
    {
        base.ApplyDamage(damage);
        UpdateUI();
    }
    public override void HealHealth(int health)
    {
        base.HealHealth(health);
        UpdateUI();
    }
    private void UpdateUI()
    {
        var healthCells = UiElementsList.instance.Counters.HealthCell;
        for(int i = 0; i < CurrentHealth; i++)
        {
            healthCells[i].Enable();
        }

        for(int i = _maxHealth - 1; i >= Mathf.Max(CurrentHealth,0); i--)
        {
            healthCells[i].Disable();
        }
    }
    public override void DestroyHimself()
    {
        LoseGame.instance.Lose();
        SoundDead();
    }
}
