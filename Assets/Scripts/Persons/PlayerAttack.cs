using System.Collections;
using UnityEngine;
using UnityEngine.UI;
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
        }
    }
    [SerializeField] private int _maxAmmo;
    [SerializeField] private float _timeReload;
    //[SerializeField] private float _attackRange;
    [SerializeField] private Text _attackText;
    public bool IsSelectedBullet;
    public int Damage;
    private GameObject _weapon;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _spawnPoinBulletNow;
    private Vector2 _shieldSpawnPointNow;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        StartCoroutine(Reload());
        _attackText.text = "Attack:" + _ammo.ToString() + "/" + _maxAmmo.ToString();
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
    public void Shot()
    {
        if (_ammo < 1)
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
        _ammo--;
        _attackText.text = "Attack:" + _ammo.ToString() + "/" + _maxAmmo.ToString();

    }
    IEnumerator Reload()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeReload);
            if (_ammo < _maxAmmo)
            {
                _ammo++;
                _attackText.text = "Attack:" + _ammo.ToString() + "/" + _maxAmmo.ToString();
            }
        }
    }
}