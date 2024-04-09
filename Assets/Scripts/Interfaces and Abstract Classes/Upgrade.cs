using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

[System.Serializable]
public class Upgrade : MonoBehaviour
{
    [HideInInspector] private bool isTaken = false;
    [SerializeField] public Sprite Sprite;
    [SerializeField] public int ID;
    public Dictionary<int, Action> Synergies = new Dictionary<int, Action>();
    [SerializeField] protected Synergy[] synergies = new Synergy[0];
    public bool IsTaken
    {
        get => LevelUP.Items[ID].isTaken;
        set { LevelUP.Items[ID].isTaken = value; }
    }
   

    public void Start()
    {
        var Actions = UpgradeButton.UpgradeActions;
        if (Actions.ContainsKey(ID))
        {
            Actions[ID] += OnTake;
        }
        else
        {
            if (LevelUP.Items.Count > ID)
                LevelUP.Items[ID] = this;
            else
            {
                while (LevelUP.Items.Count < ID + 1)
                    LevelUP.Items.Add(null);
                LevelUP.Items[ID] = this;
            }

            Actions.Add(ID, OnTake);
        }

        StartCoroutine(setSynergy());

        IEnumerator setSynergy()
        {
            yield return new WaitForEndOfFrame();
            foreach (Synergy synergy in synergies)
            {
                var LevelUPSynergies = LevelUP.Items[synergy.SynergentID].Synergies;
                if (LevelUPSynergies.ContainsKey(synergy.InitiatorID))
                    LevelUPSynergies[synergy.InitiatorID] += synergy.OnTake;
                else LevelUPSynergies.Add(synergy.InitiatorID, synergy.OnTake);
            }
        }
    }

    protected virtual void OnTake()
    {
        if(!Save.console)
        {
            PlayerPrefs.SetInt(Sprite.name, PlayerPrefs.GetInt(Sprite.name,0)+1);
            PlayerPrefs.Save();
        }
        foreach (var synergy in Synergies)
        {
            if (LevelUP.Items[synergy.Key].IsTaken) synergy.Value.Invoke();
        }
        foreach (Synergy synergy in synergies)
        {
            if (LevelUP.Items[synergy.SynergentID].IsTaken) synergy.OnTake();
        }
    }

    public Upgrade()
    {
    }
}

public class Synergy : MonoBehaviour
{
    bool isTaken = false;
    protected Upgrade Invoker => LevelUP.Items[InitiatorID];

    public bool IsWorking
    {
        get => LevelUP.Items[InitiatorID].IsTaken && LevelUP.Items[SynergentID].IsTaken;
    }

    [SerializeField] public int InitiatorID, SynergentID;

    public void OnTake()
    {
        if (!isTaken)
        {
            onTake();
            isTaken = true;
        }
    }

    protected virtual void onTake()
    {
    }
}