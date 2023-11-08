using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator)),
 RequireComponent(typeof(SpriteRenderer))]
public class PlayerAttack : MonoBehaviour
{
    [field: SerializeField] public bool IsSelectedBullet { get; set; }
    [field: SerializeField] public bool IsUltraAttack { get; set; }
    [field: SerializeField] public int Damage { get; set; }
    [SerializeField, Range(0, 1)] private float SpeedIsDamageCutout;
    [SerializeField] private GameObject _AttackSound;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Vector2 _spawnPoinBullet;
    [SerializeField] private GameObject _shield;
    [SerializeField] private GameObject _shieldUltra;
    [SerializeField] private Vector2 _shieldSpawnPoint;
    [SerializeField] private Vector2 _shieldUltraSpawnPoint;
    [SerializeField] private AmmoCell[] AmmoCell;
    [SerializeField] private int _ammo;
    [SerializeField] private int _maxAmmo;
    [SerializeField] private float _timeReload;
    private GameObject _weapon;
    private Rigidbody2D rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Player player;
    private Vector2 _spawnPoinBulletNow;
    private Vector2 _shieldSpawnPointNow;
    private Vector2 _shieldUltraSpawnPointNow;


    public bool isSpeedIsDamage
    {
        set
        {
            switch (value)
            {
                case true:
                    StartCoroutine(SpeedIsDamage());
                    break;
                case false:
                    StopCoroutine(SpeedIsDamage());
                    break;
            }
        }
    }

    public Action OnRefreshAmmo { get; set; }
    private float coefficientAttak = 0f;

    public int Ammo
    {
        get { return _ammo; }
        set
        {
            _ammo = Mathf.Min(Mathf.Max(0, value), MaxAmmo);
            OnRefreshAmmo?.Invoke();
            AmmoBarRefresh();
        }
    }

    public int MaxAmmo
    {
        get { return _maxAmmo; }
        private set { _maxAmmo = value; }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        player= GetComponent<Player>();
        OnRefreshAmmo += AmmoBarRefresh;
    }

    private void AmmoBarRefresh()
    {
        for (int i = 0; i < Ammo && i < AmmoCell.Length; i++)
        {
            AmmoCell[i].Enable();
        }

        for (int i = Ammo; i < MaxAmmo; i++)
        {
            AmmoCell[i].Disable();
        }
    }

    private void Start()
    {
        StartCoroutine(Reload());
    }

    public void OnAttack()
    {
        SetSpawnPoint();
        _weapon = Instantiate(_shield, _shieldSpawnPointNow, Quaternion.identity);
        _weapon.GetComponent<AttackProjectile>().Damage = Damage += (int) coefficientAttak;
        Instantiate(_AttackSound);
    }

    public void OnFullAttack()
    {
        SetSpawnPoint();
        _weapon = Instantiate(_bullet, _spawnPoinBulletNow, Quaternion.identity);
        _weapon.GetComponent<AttackProjectile>().Damage = Damage;
    }

    public void OnUltraAttack()
    {
        print("OnUltraAttack");

        SetSpawnPoint();
        _weapon = Instantiate(_shieldUltra, _shieldUltraSpawnPointNow, Quaternion.identity);
        _weapon.GetComponent<SpriteRenderer>().flipX = gameObject.GetComponent<SpriteRenderer>().flipX;
        _weapon.GetComponent<AttackProjectile>().Damage = Damage;
        Instantiate(_AttackSound);
        slowUp();
        player.Dash(_spriteRenderer.flipX ? 1 : -1);
    }


    IEnumerator SpeedIsDamage()
    {
        while (true)
        {
            Vector3 _transform3fago = transform.position;

            yield return new WaitForSeconds(3f);
            coefficientAttak = Vector3.Distance(transform.position, _transform3fago) * SpeedIsDamageCutout;
        }
    }

    public void Shot()
    {
        if (Ammo < 1) return;


        if (IsUltraAttack && IsSelectedBullet)
        {
            if(Ammo<5) return;
            Invoke(nameof(OnUltraAttack),0.5f);
            Invoke(nameof(OnFullAttack),0.5f);
            _animator.SetTrigger("UltraAttaka");
            Ammo-=3;
        }

        else if (IsUltraAttack)
        {
            if(Ammo < 5) return;
            Ammo -= 3;
            Invoke(nameof(OnUltraAttack),0.5f);
            _animator.SetTrigger("UltraAttaka");

        }

        else if (IsSelectedBullet)
        {
            OnFullAttack();
            OnAttack();
            _animator.SetTrigger("FullAttack");
        }
        else
        {
            OnAttack();
            _animator.SetTrigger("Attack");
        }

        Ammo--;
        AmmoBarRefresh();
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
        _spawnPoinBulletNow = _spawnPoinBullet + (Vector2) transform.position;
        _shieldSpawnPointNow = _shieldSpawnPoint + (Vector2) transform.position;
        _shieldUltraSpawnPointNow = _shieldUltraSpawnPoint + (Vector2) transform.position;
        if (_spriteRenderer.flipX)
        {
            _shieldSpawnPointNow.x = transform.position.x - _shieldSpawnPoint.x;
            _shieldUltraSpawnPointNow.x = transform.position.x - _shieldUltraSpawnPoint.x;
            _spawnPoinBulletNow.x = transform.position.x - _spawnPoinBullet.x;
        }
        else
        {
            _shieldSpawnPointNow.x = transform.position.x + _shieldSpawnPoint.x;
            _shieldUltraSpawnPointNow.x = transform.position.x + _shieldUltraSpawnPoint.x;
            _shieldUltraSpawnPointNow.x = transform.position.x + _shieldUltraSpawnPoint.x;
            _spawnPoinBulletNow.x = transform.position.x + _spawnPoinBullet.x;
        }
    }

    private IEnumerator Reload()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeReload);

            Ammo++;
            AmmoBarRefresh();
        }
    }
}