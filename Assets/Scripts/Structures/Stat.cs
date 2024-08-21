using System;
using DustyStudios.SerCollections;
using System.Linq;
using UnityEngine;
[Serializable]
public class Stat:ISerializationCallbackReceiver
{
    public delegate void OnValueChangedDelegate(float oldValue,float newValue);
    public event OnValueChangedDelegate OnValueChanged;
    public SerObservableCollection<float> summingMultiplers = new(new float[1]{1}),
        multiplingMultiplers = new(),
        additions = new();
    [SerializeField] private float baseValue = 1;
    private float lastValue = 1;
    public Stat(float baseValue) => Init(baseValue);
    public void OnAfterDeserialize() => Init(baseValue);
    public void Init(float baseValue)
    {
        this.BaseValue = baseValue;
        this.lastValue = baseValue;
        this.Value = baseValue;
        foreach(SerObservableCollection<float> collection in new[] { summingMultiplers,multiplingMultiplers,additions })
        {
            collection.CollectionChanged -= (s,args) => RecalculateValue();
            collection.CollectionChanged += (s,args) => RecalculateValue();
            collection.CollectionChanged -= (s,a) => Debug.Log(a.ToString());
            collection.CollectionChanged += (s,a) => Debug.Log(a.ToString());
        }
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
    public float Value { get; private set; }
    private void RecalculateValue()
    {
        Value = (BaseValue * Math.Max(summingMultiplers.Sum(),1) + additions.Sum()) * multiplingMultiplersResult;
        if(lastValue == Value) return;
        OnValueChanged?.Invoke(lastValue,Value);
        lastValue = Value;
    }
    public float BaseValue
    {
        get => baseValue; 
        set
        {
            if(baseValue == value) return;
            baseValue = value;
            RecalculateValue();
        }
    }
    public static implicit operator int(Stat stat)=> Convert.ToInt32(stat.Value);
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

    // Ненужная фигня
    public void OnBeforeSerialize(){}

}