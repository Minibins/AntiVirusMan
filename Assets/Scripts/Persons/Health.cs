using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 1;
    public int CurrentHealth { get; private set; }
    private Action _death;
    public void ApplyDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0 && _death != null)
        {
            _death();
            _death = null;
        }
    }
    public void SetMaxHealth(int maxHealth)
    {
        CurrentHealth += maxHealth;
        _maxHealth += maxHealth;
    }
    public void HealHealth(int health)
    {
        CurrentHealth += health;
        CurrentHealth = CurrentHealth > _maxHealth ? _maxHealth : CurrentHealth;
    }
    public void SetActionDeath(Action onDeath)
    {
        _death = onDeath;
    }
    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }
    private void DestroyHimself()
    {
        Destroy(gameObject);
    }

}
