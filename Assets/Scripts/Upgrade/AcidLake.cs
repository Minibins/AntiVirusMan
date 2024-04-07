using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AcidLake : MonoBehaviour
{
    [SerializeField] DRAG blob;
    Dictionary<DebuffBank, AcidDebuff> debuffs=new();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DebuffBank bank;
        if(collision.TryGetComponent<DebuffBank>(out bank)&&!debuffs.ContainsKey(bank))
        {
            debuffs.Add(bank,new AcidDebuff());
            bank.AddDebuff(debuffs[bank]);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        DebuffBank bank;
        if(collision.TryGetComponent<DebuffBank>(out bank)&&debuffs.ContainsKey(bank))
        {
            bank.RemoveDebuff(debuffs[bank]);
            debuffs.Remove(bank);
        }
    }
    private void OnMouseDown()
    {
        StartCoroutine(CreateBlob());
    }
    private void OnMouseUp()
    {
        otpustitBloby();
        otpustitBloby=null;
    }
    event Action otpustitBloby;
    IEnumerator CreateBlob()
    {
        DRAG blobDrag=Instantiate(blob,Camera.main.ScreenToWorldPoint(Input.mousePosition),Quaternion.identity);
        blobDrag.SetDragging();
        otpustitBloby += ()=>blobDrag.StopDragging();
        Transform blobTransform = blobDrag.transform;
        for(float i = 0; i < 1f; i+=Time.fixedDeltaTime) 
        { 
            blobTransform.localScale = Vector3.one*5*i;
            yield return new WaitForFixedUpdate();
        }
        blobTransform.localScale = Vector3.one * 5;
    }
}