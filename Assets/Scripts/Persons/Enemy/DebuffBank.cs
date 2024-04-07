using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class DebuffBank : MonoBehaviour,IScannable
{
    List<Debuff> debuffs = new();
    Animator anim, visualizer;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        visualizer = GetComponentsInChildren<Animator>().Where(a=>a!=anim).First();
        visualizer.gameObject.SetActive(false);
    }
    public void AddDebuff(Debuff debuff)
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
    Dictionary<string,bool> animatorHasBool = new();
    bool AnimatorHasBool(string name)
    {
        if(!animatorHasBool.ContainsKey(name))
            animatorHasBool.Add(name,anim.parameters.Any(p => p.name == name&&p.type==AnimatorControllerParameterType.Bool));
        return animatorHasBool[name];
    }
    void SetAnim(bool state,Debuff debuff)
    {
        if(debuff.animationName != ""&&AnimatorHasBool(debuff.animationName))
            anim.SetBool(debuff.animationName,state);
        else if(visualizer != null)
        {
            visualizer.gameObject.SetActive(state||AnyOfDebuffsUsesVisualizer!=null);
            visualizer.Play(state ? debuff.animationName:AnyOfDebuffsUsesVisualizer.animationName);
        }
    }
    Debuff AnyOfDebuffsUsesVisualizer
    {
        get
        {
            IEnumerable<Debuff> Out = debuffs.Where(d => d.animationName != "" && !AnimatorHasBool(d.animationName));
            return Out.Count()>0 ? Out.First():null;
        }
    }
    public void RemoveDebuff(Debuff debuff)
    {
        debuffs.Remove(debuff);
        debuff.Clear();
        SetAnim(false,debuff);
    }
    public void StartScan()
    {
        AddDebuff(new ScannerDebuff());
    }
    public void EndScan(){}
    public void StopScan()
    {
        List<Debuff> debufs = debuffs.Where(d => d.GetType() == typeof(ScannerDebuff)).ToList();
        foreach(Debuff debuff in debufs) 
        {
            RemoveDebuff(debuff);
        }
        debuffs = debuffs.Where(d => d.GetType()!=typeof(ScannerDebuff)).ToList();
    }
}