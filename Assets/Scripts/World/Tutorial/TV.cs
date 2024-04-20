using System.Collections;
using UnityEngine;

public class TV : TalkingPerson
{
    private Animator anim;

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        anim = GetComponent<Animator>();

        #region

        OnID(19);
        OnID(7);
        OnID(4);

        #endregion
    }

    private void OnID(int Id)
    {
        if (!UpgradeButton.UpgradeActions.ContainsKey(Id))
        {
            UpgradeButton.UpgradeActions.Add(Id, (HelloWorld));
        }
        else
        {
            UpgradeButton.UpgradeActions[Id] += HelloWorld;
        }
    }

    private void HelloWorld()
    {
        anim.SetFloat("Speed", 1);
        Talk();
    }
}