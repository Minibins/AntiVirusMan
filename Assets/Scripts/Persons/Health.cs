using System;

using Unity.VisualScripting;

using UnityEngine;
using Random = UnityEngine.Random;

public class Health : MonoBehaviour,Draggable,IDamageble
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private HealthCell[] healthCells;
    [SerializeField] public GameManager gameManager;
    [SerializeField] private GameObject DeathSound;
    [SerializeField] private GameObject PunchSound;
    private bool IsInvisible;
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private float needVelocityForInvisibility=9999;
    [field: SerializeField] public float CurrentHealth;
    private Action _onDeath;
    public static bool backStager = false;
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
    public void ApplyDamage(float damage)
    {
        if(!IsInvisible)
        {
        Instantiate(PunchSound);
        if (gameObject.name == "PC")
        {
            CurrentHealth -= damage;
        }
        else
        {
            if (backStager == true)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                Enemy enemy = GetComponent<Enemy>();
                if (enemy.moveDirection == -1f && player.transform.position.x > transform.position.x)
                {
                    int i = Random.Range(1, 5);
                    if (i == 1)
                    {
                        CurrentHealth -= damage * 3f;
                        print("DAMAGE: 3X");
                    }
                }
                else if (enemy.moveDirection == 1f && player.transform.position.x < transform.position.x)
                {
                    int i = Random.Range(1, 5);
                    if (i == 1)
                    {
                        CurrentHealth -= damage * 3f;
                        print("DAMAGE: 3X");
                    }
                }
            }
            else
            {
                CurrentHealth -= damage;
            }
        }
        OnApplyDamage?.Invoke();
        if (CurrentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
        if(gameObject.name == "PC")
        {
            for(int i = 0; i < CurrentHealth; i++)
            {
                healthCells[i].Enable();
            }
            for(int i = _maxHealth - 1; i >= CurrentHealth; i--)
            {
                healthCells[i].Disable();
            }
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
            for (int i = 0; i < CurrentHealth; i++)
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
        rb=GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
        {Destroy(gameObject);
            GetComponent<Enemy>()._PC.GetComponentInChildren<PC>().EnemyKilled();
        }
        
    }
    private void FixedUpdate()
    {
            if(rb.velocity.magnitude < needVelocityForInvisibility)
            {
                IsInvisible = false;
                animator.SetBool("IsInvisible",false);
            }
            else
            {
                IsInvisible = true;
                animator.SetBool("IsInvisible",true );
            }
    }
    
    public void SoundDead()
    {
        Instantiate(DeathSound);
    }
    public void OnDrag()
    {
    }
    public void OnDragEnd()
    {
        IsInvisible = false;
    }
    public void OnDamageGet(int Damage)
    {
        ApplyDamage( Damage);
    }
}