using System;
using System.Collections;
using System.Collections.Generic;

using DustyStudios.MathAVM;

using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator)),
 RequireComponent(typeof(SpriteRenderer))]

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] public AbstractAttack MainAttack;
    [HideInInspector] public List<AbstractAttack> TemporaryAttackSubstitute = new();
    AbstractAttack FirstAttack => TemporaryAttackSubstitute.Count > 0 ? TemporaryAttackSubstitute[0] :MainAttack;
    [SerializeField] public List<AbstractAttack> AdditionalAttacks;
    [SerializeField] public Stat Damage;
    [SerializeField] private int _ammo, _maxAmmo;
    [SerializeField] public float _timeReload;
    private Rigidbody2D rb;
    private Animator _animator;
    public Animator Animator { get => _animator; }
    private SpriteRenderer _spriteRenderer;
    Scanner scanner;
    private PC pc;

    public Action OnRefreshAmmo { get; set; }
    public void AddTemporaryAttackSubstitute(AbstractAttack substitute)
    {
        if(substitute.isUsingJoystick) UiElementsList.instance.Joysticks.Attack.gameObject.SetActive(true);
        TemporaryAttackSubstitute.Add(substitute);
    }
    public int Ammo
    {
        get { return _ammo; }
        set
        {
            _ammo = Mathf.Min(Mathf.Max(0, value), MaxAmmo);
            OnRefreshAmmo?.Invoke();
            AmmoBarRefresh();
            StartCoroutine(Reload());
        }
    }

    public int MaxAmmo
    {
        get { return _maxAmmo; }
        private set { _maxAmmo = value; }
    }

    public Rigidbody2D Rb { get => rb; set => rb = value; }
    public Rigidbody2D Rb1 { get => rb; set => rb = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        scanner = GetComponentInChildren<Scanner>();
        OnRefreshAmmo += AmmoBarRefresh;
        pc = GameObject.FindGameObjectWithTag("PC").GetComponentInChildren<PC>();
    }

    private void AmmoBarRefresh()
    {
        var AmmoCell= UiElementsList.instance.Counters.AmmoCell;
        for (int i = 0; i < Ammo && i < AmmoCell.Length; i++)
        {
            AmmoCell[i].Enable();
        }

        for (int i = Ammo; i < MaxAmmo; i++)
        {
            AmmoCell[i].Disable();
        }
    }

    public void EndScanner()
    {
        scanner.EndAttack();
    }
    public void StartScanner()
    {
        AddTemporaryAttackSubstitute( scanner);
        Shot();
    }
    public void StopAttack()
    {
        _animator.SetBool("Attack",false);
        FirstAttack.StopAttack();
        if(TemporaryAttackSubstitute.Count > 0) 
        TemporaryAttackSubstitute.RemoveAt(0);
    }
    public void Shot()
    {
        WaitForPlayerAttack.Shot();
        if (Ammo < FirstAttack.AmmoCost) return;
        FirstAttack.Attack(FirstAttack.LoadTime);
        Ammo -= FirstAttack.AmmoCost;
        
        var Joystick = UiElementsList.instance.Joysticks.Attack;
        if(FirstAttack.isUsingJoystick)
            transform.localScale = new(Mathf.Abs(transform.localScale.x)* MathA.OneOrNegativeOne(Joystick.Horizontal < 0),transform.localScale.y,transform.localScale.z);
        Joystick.gameObject.SetActive(MainAttack.isUsingJoystick);
        if(FirstAttack.allowOtherAttacks)
        {
            foreach(AbstractAttack attack in AdditionalAttacks)
            {
                Ammo -= attack.AmmoCost;
                attack.Attack(FirstAttack.LoadTime+attack.LoadTime);
            }
        }
        AmmoBarRefresh();
        _animator.SetBool("Attack",true);
    }

    public void slowdown()
    {
        rb.bodyType = RigidbodyType2D.Static;
        pc.OnlyBehind = true;
    }

    public void slowUp()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnDestroy()
    {
        OnRefreshAmmo = null;
    }
    bool isOnReload;
    private IEnumerator Reload()
    {
        if(!isOnReload && Ammo < MaxAmmo)
        {
            isOnReload = true;
            while(Ammo < MaxAmmo)
            {
                yield return new WaitForSeconds(_timeReload);
                Ammo++;
            }
            isOnReload = false;
        }
    }
}