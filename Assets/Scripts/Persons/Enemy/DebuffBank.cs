using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DebuffBank : MonoBehaviour, IScannable
{
    List<Debuff> debuffs = new List<Debuff>();
    Animator anim, visualizer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        visualizer = GetComponentsInChildren<Animator>().Where(a => a != anim).First();
        visualizer.gameObject.SetActive(false);
    }

    public void AddDebuff(Debuff debuff)
    {
        if (debuff.canStack || !HasDebuffOfType(debuff.GetType()))
        {
            debuffs.Add(debuff);
            debuff.OnAdd(this);
            SetAnim(true, debuff);
            StartCoroutine(ClearDebuff());

            IEnumerator ClearDebuff()
            {
                yield return new WaitForSeconds(debuff.time);
                RemoveDebuff(debuff);
            }
        }
    }

    private Dictionary<string, bool> animatorHasBool = new Dictionary<string, bool>();

    private bool AnimatorHasBool(string name)
    {
        if (!animatorHasBool.ContainsKey(name))
            animatorHasBool.Add(name,
                anim.parameters.Any(p => p.name == name && p.type == AnimatorControllerParameterType.Bool));
        return animatorHasBool[name];
    }

    private void SetAnim(bool state, Debuff debuff)
    {
        if (debuff.animationName != "" && AnimatorHasBool(debuff.animationName))
            anim.SetBool(debuff.animationName, state);
        else if (visualizer != null)
        {
            Debuff AnyOfDebuffs = AnyOfDebuffsUsesVisualizer;
            visualizer.gameObject.SetActive(state || AnyOfDebuffs != null);
            if (AnyOfDebuffs != null)
                visualizer.Play(state ? debuff.animationName : AnyOfDebuffs.animationName);
        }
    }

    private Debuff AnyOfDebuffsUsesVisualizer
    {
        get
        {
            IEnumerable<Debuff> Out = debuffs.Where(d => d.animationName != "" && !AnimatorHasBool(d.animationName));
            return Out.Count() > 0 ? Out.First() : null;
        }
    }

    public void RemoveDebuff(Debuff debuff)
    {
        debuffs.Remove(debuff);
        debuff.Clear();
        SetAnim(false, debuff);
    }

    public bool HasDebuffOfType(Type type) => debuffs.Any(d => d.GetType() == type);

    public void StartScan()
    {
        AddDebuff(new ScannerDebuff());
    }

    public void EndScan()
    {
    }

    public void StopScan()
    {
        List<Debuff> debufs = debuffs.Where(d => d.GetType() == typeof(ScannerDebuff)).ToList();
        foreach (Debuff debuff in debufs)
        {
            RemoveDebuff(debuff);
        }

        debuffs = debuffs.Where(d => d.GetType() != typeof(ScannerDebuff)).ToList();
    }
}