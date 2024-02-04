using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
[System.Serializable] public class Upgrade : MonoBehaviour
{
    [HideInInspector] private bool isTaken = false;
    [SerializeField] public Sprite Sprite;
    [SerializeField] public int ID;
    public Dictionary<int, Action> Synergies = new Dictionary<int, Action>();
    [SerializeField] protected Synergy[] synergies =new Synergy[0];

    public bool IsTaken 
    { 
        get => isTaken;
        set
        {
            isTaken = value;
        }
    }

    public void Start()
    {
        var Actions = UpgradeButton.UpgradeActions;
        if(Actions.ContainsKey(ID))
        {
            Actions[ID]+=OnTake;
        }
        else
        {
            if(LevelUP.Items.Count > ID) 
                LevelUP.Items[ID] = this;
            else
            {
                while(LevelUP.Items.Count< ID+1)
                LevelUP.Items.Add(null);
                LevelUP.Items[ID] = this;
            }
            Actions.Add(ID,OnTake);
        }
        if(synergies.Length!=0)
            foreach(Synergy synergy in synergies)
            {
                LevelUP.Items[synergy.ID].Synergies.Add(ID,synergy.OnTake);
            }
    }
    protected virtual void OnTake()
    {
        foreach (var synergy in Synergies)
        {
            if(LevelUP.Items[synergy.Key].IsTaken) synergy.Value.Invoke();
        }
    }
    public Upgrade()
    {

    }
}
public class Synergy : MonoBehaviour
{
    protected Upgrade Invoker => LevelUP.Items[ID];
    [SerializeField] public int ID;

    public virtual void OnTake()
    {
        
    }
}