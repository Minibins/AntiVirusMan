using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 1;
    [field: SerializeField] public int CurrentHealth { get; private set; }
    private Action _onDeath;
    public Action OnDeath
    {
        get => _onDeath;
        set
        {
            if (_onDeath != null)
            {
                _onDeath -= DestroyHimself;
            }
            _onDeath = value;
        }
    }
    public Action OnApplyDamage { get; set; }
    public void ApplyDamage(int damage)
    {
        CurrentHealth -= damage;
        OnApplyDamage?.Invoke();
        if (CurrentHealth <= 0)
        {
            OnDeath?.Invoke();
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
    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }
    private void Start()
    {
        if (OnDeath == null)
        {
            OnDeath = DestroyHimself;
        }
    }
    private void OnDestroy()
    {
        OnDeath = null;
        OnApplyDamage = null;
    }
    private void DestroyHimself()
    {
        Destroy(gameObject);
    }

}
