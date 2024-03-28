using System.Collections;
using System.Collections.Generic;
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
        SetAnim(true);
        StartCoroutine(ClearDebuff());
        IEnumerator ClearDebuff()
        {
            yield return new WaitForSeconds(debuff.time);
            debuffs.Remove(debuff);
            debuff.Clear();
            SetAnim(false);
        }
        void SetAnim(bool state) 
        {
            if(debuff.animationName != "") 
            {
                anim.SetBool(debuff.animationName,state); 
            } 
        }
    }
    public void StartScan()
    {
        AddDebuff(new ScannerDebuff());
    }


    public void EndScan(){}
    public void StopScan(){}
}