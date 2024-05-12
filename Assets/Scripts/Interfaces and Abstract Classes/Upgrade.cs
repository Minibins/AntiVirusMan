using System;
using System.Collections;
using System.Collections.Generic;
using DustyStudios;
using UnityEngine;

[Serializable]
public class Upgrade : MonoBehaviour
{
    private bool isTaken = false;
    [SerializeField] private Sprite Sprite;
    [SerializeField] public SpriteContainingSO anotherSprite;
    public Sprite sprite
    {
        get => Sprite;
        set
        {
            if (spriteName == null) spriteName = Sprite.name;
            Sprite = value;
        }
    }

    private string spriteName;
    private string PlayerPrefsName => spriteName != null ? spriteName : Sprite.name;
    [SerializeField] private int ID;

    public int Id
    {
        get => ID;
    }

    public Dictionary<int, Action> Synergies = new Dictionary<int, Action>();
    [SerializeField] protected Synergy[] synergies = new Synergy[0];

    public bool IsTaken
    {
        get => LevelUP.Items[Id].isTaken;
    }


    public void Start()
    {
        var Actions = UpgradeButton.UpgradeActions;
        if (Actions.ContainsKey(Id))
            Actions[Id] += OnTake;
        else
        {
            if (LevelUP.Items.Count > Id)
                LevelUP.Items[Id] = this;
            else
            {
                while (LevelUP.Items.Count < Id + 1)
                    LevelUP.Items.Add(null);
                LevelUP.Items[Id] = this;
            }

            Actions.Add(Id, OnTake);
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
        isTaken = true;
        if (!DustyConsoleInGame.UsedConsoleInSession)
        {
            PlayerPrefs.SetInt(PlayerPrefsName, PlayerPrefs.GetInt(PlayerPrefsName, 0) + 1);
            PlayerPrefs.Save();
        }

        foreach (var synergy in Synergies)
            if (LevelUP.Items[synergy.Key].IsTaken) synergy.Value.Invoke();

        foreach (Synergy synergy in synergies)
            if (LevelUP.Items[synergy.SynergentID].IsTaken) synergy.OnTake();
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