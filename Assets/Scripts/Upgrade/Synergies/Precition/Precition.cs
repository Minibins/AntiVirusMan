using System.Collections;
using UnityEngine;

public class Precition : Synergy
{
    [SerializeField] Behaviour[] behaviours;
    [SerializeField] float timeForEnable;
    protected override void onTake()=>
        StartCoroutine(wait());
    IEnumerator wait()
    {
        while(true)
        {
            foreach(var behaviour in behaviours)
                behaviour.enabled = false;
            yield return new WaitForPlayerAttack();
            foreach(var behaviour in behaviours)
                behaviour.enabled = true;
            yield return new WaitForSeconds(timeForEnable);
        }
    }
}
