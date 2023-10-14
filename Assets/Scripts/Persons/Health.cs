using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private HealthCell[] healthCells;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private GameObject DeathSound;
    [SerializeField] private GameObject PunchSound;
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
        Instantiate(PunchSound);
        CurrentHealth -= damage;
        OnApplyDamage?.Invoke();
        if (CurrentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
        else if (gameObject.name == "PC")
        {
            for (int i = _maxHealth - 1; i >= CurrentHealth; i--)
            {
                healthCells[i].Disable();
            }
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
        if (gameObject.name == "PC")
        {
            for (int i = 0; i < healthCells.Length; i++)
            {
                healthCells[i].Enable();
            }
        }
    }
    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }
    private void Start()
    {

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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
        if (gameObject.name == "PC")
        {
            gameManager.LoseGame();
            Instantiate(DeathSound);
        }
        else
        {
            GetComponent<Enemy>()._PC.GetComponentInChildren<PC>().EnemyKilled();
        }
        Destroy(gameObject);
    }

    public void SoundDead()
    {
        Instantiate(DeathSound);
    }
}