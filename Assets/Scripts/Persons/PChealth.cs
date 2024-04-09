using DustyStudios;

using UnityEngine;

public class PChealth : DefaultHealth
{
    public static PChealth instance;
    override protected void Awake()
    {
        base.Awake();
        instance = this;
    }
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
    [DustyConsoleCommand("health","Set PC health into value", typeof(int))]
    public static string SetHealth(int health)
    {
        instance.CurrentHealth = health;
        instance.UpdateUI();
        return $"Health are {health} now";
    }
    [DustyConsoleCommand("inv","Gain invincibility if 1, lose if 0",typeof(int))]
    public static string Invicibility(int value)
    {
        switch(value)
        {
            case -1:
            SetHealth(1);
            return "RIP";
            case 0:
            instance.needVelocityForInvisibility = 18;
            return "now you're vulnerable";
            case 1:
            instance.needVelocityForInvisibility = 0;
            return "now you're invicibile";
            default:
            if(value < -1)
            {
                instance.ApplyDamage(999);
                return "RIP";
            }
            return "wrong digit";
        }
    }
}
