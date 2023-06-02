using System;
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Animator)),
    RequireComponent(typeof(SpriteRenderer))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Vector2 _spawnPoinBullet;
    [SerializeField] private GameObject _shield;
    [SerializeField] private Vector2 _shieldSpawnPoint;
    [SerializeField] private int _ammo;
    [SerializeField] private int _maxAmmo;
    [SerializeField] private float _timeReload;
    [field: SerializeField] public bool IsSelectedBullet { get; set; }
    [field: SerializeField] public int Damage { get; set; }
    private GameObject _weapon;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _spawnPoinBulletNow;
    private Vector2 _shieldSpawnPointNow;
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
        _weapon.GetComponent<AttackProjectile>().Damage = Damage;
    }
    public void OnFullAttack()
    {
        SetSpawnPoint();
        _weapon = Instantiate(_bullet, _spawnPoinBulletNow, Quaternion.identity);
        _weapon.GetComponent<AttackProjectile>().Damage = Damage;
        _weapon = Instantiate(_shield, _shieldSpawnPointNow, Quaternion.identity);
        _weapon.GetComponent<AttackProjectile>().Damage = Damage;
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
            }
        }
    }
}