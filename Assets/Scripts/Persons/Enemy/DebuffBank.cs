using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DebuffBank : MonoBehaviour,IScannable
{
    List<Debuff> debuffs = new();
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
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
    void SetAnim(bool state,Debuff debuff)
    {
        if(debuff.animationName != "")
        {
            anim.SetBool(debuff.animationName,state);
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