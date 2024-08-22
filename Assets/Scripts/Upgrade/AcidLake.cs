using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;
public class AcidLake : MonoBehaviour
{
    [SerializeField] DRAG blob;
    Dictionary<DebuffBank, AcidDebuff> debuffs=new();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        WorkWithDebuffBank(collision,bank =>
        {
            if(!debuffs.ContainsKey(bank))
            {
                debuffs.Add(bank,new AcidDebuff());
                bank.AddDebuff(debuffs[bank]);
            }
        });
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        WorkWithDebuffBank(collision,bank =>
        {
            if(debuffs.ContainsKey(bank))
            {
                bank.RemoveDebuff(debuffs[bank]);
                debuffs.Remove(bank);
            }
        });
    }

    void WorkWithDebuffBank(Collider2D collision, Action<DebuffBank> action)
    {
        DebuffBank bank;
        if(collision.TryGetComponent<DebuffBank>(out bank))
            action(bank);
    }
    private void OnMouseDown()
    {
        StartCoroutine(CreateBlob());
    }
    private void OnMouseUp()
    {
        if(otpustitBloby != null && !otpustitBloby.IsDestroyed())
            otpustitBloby.StopDragging();
        otpustitBloby=null;
    }
    DRAG otpustitBloby;
    IEnumerator CreateBlob()
    {
        otpustitBloby = Instantiate(blob,(Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition),Quaternion.identity);
        otpustitBloby.SetDragging();
        Transform blobTransform = otpustitBloby.transform;
        for(float i = 0; i < 1f; i+=Time.fixedDeltaTime)
        {
            if(blobTransform.IsDestroyed()) yield break;
            setLocalScale(i);
            yield return new WaitForFixedUpdate();
        }
        setLocalScale(1);
        void setLocalScale(float scale)
        {
            if(blobTransform.IsDestroyed()) return;
            blobTransform.localScale = Vector3.one * 5 * scale;
        }
    }
}