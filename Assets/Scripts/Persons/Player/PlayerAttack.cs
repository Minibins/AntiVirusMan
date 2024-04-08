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
    [SerializeField] private int _maxAmmo;
    [SerializeField] public float _timeReload;
    private Rigidbody2D rb;
    private Animator _animator;
    public Animator Animator { get => _animator; }
    private SpriteRenderer _spriteRenderer;
    Scanner scanner;
    private PC pc;
    public bool stopAttackOnAnimationEnd = true;
    public void AddTemporaryAttackSubstitute(AbstractAttack substitute)
    {
        if(substitute.isUsingJoystick) UiElementsList.instance.Joysticks.Attack.gameObject.SetActive(true);
        TemporaryAttackSubstitute.Add(substitute);
    }
    public RechargingValue Ammo;
    public Rigidbody2D Rb { get => rb; set => rb = value; }
    public Rigidbody2D Rb1 { get => rb; set => rb = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        scanner = GetComponentInChildren<Scanner>();
        pc = GameObject.FindObjectOfType<PC>();
        Ammo = new(new(_maxAmmo,0),_maxAmmo,_timeReload,1,value=>new WaitForSeconds(value));
        Ammo.ValueChanged += AmmoBarRefresh;
    }

    private void AmmoBarRefresh(float Ammo)
    {
        var AmmoCell= UiElementsList.instance.Counters.AmmoCell;
        for (int i = 0; i < Ammo && i < AmmoCell.Length; i++)
        {
            AmmoCell[i].Enable();
        }

        for (int i = (int)Ammo; i < _maxAmmo && i < AmmoCell.Length; i++)
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
        if(stopAttackOnAnimationEnd)
            StopAttack();
    }
}