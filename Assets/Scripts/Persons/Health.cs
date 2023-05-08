using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 1;
    public int CurrentHealth { get; private set; }
    private Action _onDeath;
    private Action _onApplyDamage;
    public void ApplyDamage(int damage)
    {
        CurrentHealth -= damage;
        _onApplyDamage?.Invoke();
        if (CurrentHealth <= 0)
        {
            _onDeath();
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
    public void SetActionApplyDamage(Action onApplyDamage)
    {
        _onApplyDamage = onApplyDamage;
    }
    public void SetActionDeath(Action onDeath)
    {
        _onDeath = onDeath;
    }
    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }
    private void OnEnable()
    {
        _onDeath ??= DestroyHimself;
    }
    private void OnDisable()
    {
        _onDeath = null;
        _onApplyDamage = null;
    }
    private void DestroyHimself()
    {
        Destroy(gameObject);
    }

}
