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
    public int Ammo
    {
        get
        {
            return _ammo;
        }
        set
        {
            _ammo = value;
            _onRefreshAmmo();
        }
    }
    [SerializeField] private int _maxAmmo;
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
    [SerializeField] private float _timeReload;
    public bool IsSelectedBullet;
    public int Damage;
    private GameObject _weapon;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _spawnPoinBulletNow;
    private Vector2 _shieldSpawnPointNow;
    private Action _onRefreshAmmo;
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
    public void SetActionOnRefreshAmmo(Action onRefreshAmmo)
    {
        _onRefreshAmmo = onRefreshAmmo;
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
    private void OnDisable()
    {
        _onRefreshAmmo = null;
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