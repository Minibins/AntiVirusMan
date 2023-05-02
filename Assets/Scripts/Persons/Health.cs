using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 1;
    private int _currentHealth;
    private void Awake()
    {
        _currentHealth = _maxHealth;
    }
    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void SetMaxHealth(int maxHealth)
    {
        _currentHealth += maxHealth;
        _maxHealth += maxHealth;
    }
    public void HealHealth(int health)
    {
        _currentHealth += health;
        _currentHealth = _currentHealth > _maxHealth ? _maxHealth : _currentHealth;
    }
}
