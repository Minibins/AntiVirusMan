using System;
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Animator)),
    RequireComponent(typeof(SpriteRenderer))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _AttackSound;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Vector2 _spawnPoinBullet;
    [SerializeField] private GameObject _shield;
    [SerializeField] private Vector2 _shieldSpawnPoint;
    [SerializeField] private AmmoCell[] AmmoCell;
    [SerializeField] private int _ammo;
    [SerializeField] private int _maxAmmo;
    [SerializeField] private float _timeReload;
    [field: SerializeField] public bool IsSelectedBullet { get; set; }
    [field: SerializeField] public int Damage { get; set; }
    private GameObject _weapon;
    private Rigidbody2D rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _spawnPoinBulletNow;
    private Vector2 _shieldSpawnPointNow;
    public bool isSpeedIsDamage;
    public Action OnRefreshAmmo { get; set; }
    public int Ammo
    {
        get
        {
            return _ammo;
        }
        set
        {
            _ammo = value;
            OnRefreshAmmo?.Invoke();
        }
    }
    public int MaxAmmo
    {
        get
        {
            return _maxAmmo;
        }
        private set
        {
            _maxAmmo = value;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        StartCoroutine(Reload());
    }

    public void OnAttack()
    {
        SetSpawnPoint();
        _weapon = Instantiate(_shield, _shieldSpawnPointNow, Quaternion.identity);
        if (isSpeedIsDamage)
        {
            _weapon.GetComponent<AttackProjectile>().Damage = Damage + (int)Mathf.Pow((Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.y, 2)), 0.5f);
        }
        else
        {
            _weapon.GetComponent<AttackProjectile>().Damage = Damage;
        }
        Instantiate(_AttackSound);
    }
    public void OnFullAttack()
    {
        SetSpawnPoint();
        _weapon = Instantiate(_bullet, _spawnPoinBulletNow, Quaternion.identity);
        _weapon.GetComponent<AttackProjectile>().Damage = Damage;
        _weapon = Instantiate(_shield, _shieldSpawnPointNow, Quaternion.identity);
        if (isSpeedIsDamage)
        {
            _weapon.GetComponent<AttackProjectile>().Damage = Damage + (int)Mathf.Pow((Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.y, 2)), 0.5f);
        }
        else
        {
            _weapon.GetComponent<AttackProjectile>().Damage = Damage;
        }
        Instantiate(_AttackSound);
    }
    public void Shot()
    {
        if (Ammo < 1)
        {
            return;
        }
        if (IsSelectedBullet)
        {
            OnFullAttack();
            _animator.SetTrigger("FullAttack");
        }
        else
        {
            OnAttack();
            _animator.SetTrigger("Attack");
        }
        Ammo--;
        for (int i = _maxAmmo - 1; i >= Ammo; i--)
        {
            AmmoCell[i].Disable();
        }

    }
    public void slowdown()
    {
        rb.bodyType = RigidbodyType2D.Static;
        print("slowDown");
    }

    public void slowUp()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnDestroy()
    {
        OnRefreshAmmo = null;
    }
    private void SetSpawnPoint()
    {
        _spawnPoinBulletNow = _spawnPoinBullet + (Vector2)transform.position;
        _shieldSpawnPointNow = _shieldSpawnPoint + (Vector2)transform.position;
        if (_spriteRenderer.flipX)
        {
            _shieldSpawnPointNow.x = transform.position.x - _shieldSpawnPoint.x;
            _spawnPoinBulletNow.x = transform.position.x - _spawnPoinBullet.x;
        }
        else
        {
            _shieldSpawnPointNow.x = transform.position.x + _shieldSpawnPoint.x;
            _spawnPoinBulletNow.x = transform.position.x + _spawnPoinBullet.x;
        }
    }

    private IEnumerator Reload()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeReload);
            if (Ammo < _maxAmmo)
            {
                Ammo++;
                //for (int i = 0; i < AmmoCell.Length; i++)
                //{
                //   AmmoCell[i].Enable();
                // }

                for (int i = 0; i < Ammo && i < AmmoCell.Length; i++)
                {
                    AmmoCell[i].Enable();
                }
            }
        }
    }
}