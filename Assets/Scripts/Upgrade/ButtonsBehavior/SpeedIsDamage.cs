using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class SpeedIsDamage : Upgrade
{
    NewInputSystem _newInputSystem;
    [SerializeField] Transform followingTransform;
    Stat playerDamage;
    [SerializeField] float DamageMultipler, maxTime;
    struct Keypos
    {
        public float time;
        public Vector3 pos;

        public Keypos(float time,Vector3 pos)
        {
            this.time = time;
            this.pos = pos;
        }
    }
    List<Keypos> keyposes = new();
    protected override void OnTake()
    {
        base.OnTake();
        playerDamage = GameObject.FindAnyObjectByType<PlayerAttack>().Damage;
        _newInputSystem = new();
        _newInputSystem.Basic.Move.performed += context => AddKeypos();
        _newInputSystem.Basic.Move.canceled += context => AddKeypos();
        _newInputSystem.Basic.Jump.performed += context => AddKeypos();
        _newInputSystem.Basic.Jump.canceled += context => AddKeypos();
        _newInputSystem.Basic.Dash.performed += context => AddKeypos();
        _newInputSystem.Basic.Attack.performed += context => CalculateMultipler();
        _newInputSystem.Enable();
    }
    void AddKeypos()
    {
        keyposes.Add(new(Time.time,followingTransform.position));
    }
    void CalculateMultipler()
    {
        keyposes = keyposes.Where(k => (Time.time - k.time) < maxTime).ToList();
        float multipler = 0;
        for(int i = 0; i < keyposes.Count - 1; i++)
        {
            multipler += Vector3.Distance(keyposes[i].pos,keyposes[i + 1].pos);
        }
        multipler *= DamageMultipler;
        try
        {
            playerDamage.summingMultiplers[0] = multipler;
        }
        catch
        {
            playerDamage.summingMultiplers.Add(multipler);
        }
    }
}