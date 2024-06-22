using System;
using DustyStudios.SerCollections;
using System.Linq;
using UnityEditor;
using UnityEngine;
[Serializable]
public class Stat
{
    public delegate void OnValueChangedDelegate(float oldValue, float newValue);
    public event OnValueChangedDelegate OnValueChanged;
    public SerObservableCollection<float> summingMultiplers = new(new float[1]{1}),
        multiplingMultiplers = new(),
        additions = new();
    [SerializeField] private float baseValue = 1;
    private float lastValue = 1;
    public Stat(float baseValue)
    {
        this.BaseValue = baseValue;
        this.lastValue = baseValue;
        foreach(SerObservableCollection<float> collection in new[]{ summingMultiplers,multiplingMultiplers,additions})
            collection.CollectionChanged += (s,e) => AnyCollectionChanged();
    }
    public void AnyCollectionChanged()
    {
        if(lastValue == Value) return;
        OnValueChanged?.Invoke(lastValue,Value);
        lastValue = Value;
    }
    public float multiplingMultiplersResult
    {
        get => multiplingMultiplers.Count switch
            {
                0 => 1,
                1 => multiplingMultiplers[0],
                _ => multiplingMultiplers.Aggregate((x,y) => x * y)
            };
    }
    public float Value
    {
        get => (BaseValue * Math.Max(summingMultiplers.Sum(), 1) + additions.Sum()) * multiplingMultiplersResult;
    }
    public float BaseValue
    {
        get => baseValue; 
        set
        {
            if(baseValue == value) return;
            OnValueChanged?.Invoke(baseValue, value);
            lastValue = Value;
            baseValue = value;
        }
    }
    public static implicit operator int(Stat stat)=> (int) stat.Value;
    public static implicit operator float(Stat stat)
    {
        try
        {
            return stat.Value;
        }
        catch
        {
            Debug.LogError(stat);
        }
        return default;
    }
    public override string ToString()=>$"Value = {Value}, Base Value = {BaseValue}, Sum of multiplers = {summingMultiplers.Sum()}, Sum of additions = {additions.Sum()}";
}