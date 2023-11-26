using System;
using System.Collections;

using MathAVM;

using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator)),
 RequireComponent(typeof(SpriteRenderer))]
public class PlayerAttack : MonoBehaviour
{
    [field: SerializeField] public bool IsSelectedBullet { get; set; }

    public enum attackTypes
    {
        Standard,
        Ultra,
        Laser
    }

    [field: SerializeField] public attackTypes AttackType { get; set; }
    [field: SerializeField] public int Damage { get; set; }
    [SerializeField, Range(0, 1)] private float SpeedIsDamageCutout;
    [SerializeField] private GameObject _AttackSound;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Vector2 _spawnPoinBullet;
    [SerializeField] private GameObject _shield;
    [SerializeField] private GameObject _shieldUltra, LaserPrefab;
    [SerializeField] private Joystick Joystick;
    [SerializeField] private Vector2 _shieldSpawnPoint;
    [SerializeField] private Vector2 _shieldUltraSpawnPoint;
    [SerializeField] private AmmoCell[] AmmoCell;
    [SerializeField] private int _ammo;
    [SerializeField] private int _maxAmmo;
    [SerializeField] public float _timeReload;
    private Rigidbody2D rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Player player;

    private Vector2 _spawnPoinBulletNow,
        _shieldSpawnPointNow,
        _shieldUltraSpawnPointNow;

    private PC pc;

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
    public float[] coefficientAttack = {0f, 0f};

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
        pc = GameObject.FindGameObjectWithTag("PC").GetComponentInChildren<PC>();
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponent<Player>();
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

    void OnAttack()
    {
        SetSpawnPoint();
        GameObject _weapon = Instantiate(_shield, _shieldSpawnPointNow, Quaternion.identity);
        _weapon.GetComponent<AttackProjectile>().Damage = Damage + (int) (coefficientAttack[0] + coefficientAttack[1]);
        Instantiate(_AttackSound);
    }

    void OnFullAttack()
    {
        SetSpawnPoint();
        GameObject _weapon = Instantiate(_bullet, _spawnPoinBulletNow, Quaternion.identity);
        _weapon.GetComponent<AttackProjectile>().Damage = Damage;
    }

    void OnUltraAttack()
    {
        SetSpawnPoint();
        GameObject _weapon = Instantiate(_shieldUltra, _shieldUltraSpawnPointNow, Quaternion.identity);
        _weapon.GetComponent<SpriteRenderer>().flipX = _spriteRenderer.flipX;
        _weapon.GetComponent<AttackProjectile>().Damage = Damage;
        Instantiate(_AttackSound);
        slowUp();
        player.Dash(_spriteRenderer.flipX ? 1 : -1);
    }

    public void CreateLaser()
    {
        SetSpawnPoint();
        Vector2 Rotatedvec = MathA.RotatedVector(_shieldSpawnPoint, Joystick.Direction);
        GameObject _weapon = Instantiate(LaserPrefab,
            (Vector2) transform.position + Rotatedvec,
            MathA.VectorsAngle(Rotatedvec));
        if (GetComponent<Move>().Velocity.x != 0) return;
        _spriteRenderer.flipX = true;
    }

    IEnumerator SpeedIsDamage()
    {
        while (true)
        {
            Vector3 _transform3fago = transform.position;

            yield return new WaitForSeconds(3f);
            coefficientAttack[0] = Vector3.Distance(transform.position, _transform3fago) * SpeedIsDamageCutout;
        }
    }

    public void Shot()
    {
        if (Ammo < 1) return;

        switch (AttackType)
        {
            case attackTypes.Standard:
                OnAttack();
                if (IsSelectedBullet)
                {
                    OnFullAttack();
                    _animator.SetTrigger("FullAttack");
                }
                else
                {
                    _animator.SetTrigger("Attack");
                }

                break;
            case attackTypes.Ultra:
                if (Ammo < 5) return;
                Ammo -= 3;
                _animator.SetTrigger("UltraAttaka");
                Invoke(nameof(OnUltraAttack), 0.5f);
                if (IsSelectedBullet)
                {
                    Invoke(nameof(OnFullAttack), 0.5f);
                }

                break;
            case attackTypes.Laser:
                _spriteRenderer.flipX = Joystick.Horizontal < 0;
                _animator.SetTrigger("Attack");
                AttackType = LevelUP.isTaken[17] ? attackTypes.Ultra : attackTypes.Standard;
                Joystick.gameObject.SetActive(false);
                return;
        }

        Ammo--;
        AmmoBarRefresh();
    }

    public void slowdown()
    {
        rb.bodyType = RigidbodyType2D.Static;
        pc.OnlyBehind = true;
    }

    public void slowUp()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        _animator.SetBool("IsChad", false);
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