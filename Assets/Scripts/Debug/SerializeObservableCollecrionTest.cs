using DustyStudios.SerCollections;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializeObservableCollecrionTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SerObservableCollection<int> collection = new();
        collection.CollectionChanged += (s,a) => Debug.Log("collection has changed");
        collection.Add(1);

        Stat stat = new(1);
        stat.OnValueChanged+=(o,n) => Debug.Log($"Stat has changed from {o} to {n}");
        stat.BaseValue = 2;
        stat.multiplingMultiplers.Add(2);
        stat.summingMultiplers.Add(2);
        stat.additions.Add(stat);
    }
}
