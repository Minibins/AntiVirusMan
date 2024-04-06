using System.Collections;
using System.Collections.Generic;

using DustyStudios;

using UnityEngine;

public class FixedProgression : LevelUP
{
    [System.Serializable] public struct UpgradeList
    {
        [SerializeField] public int First, Second, Third;
        public void Generate()
        {
            LevelUP.Generate(First, Second, Third);
        }
    }
    [SerializeField] List<UpgradeList> Upgrades = new List<UpgradeList>();
    public override void NewUpgrade()
    {
        Upgrades[pickedItems.Count].Generate();
    }
}
