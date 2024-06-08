using System;
using System.Collections.ObjectModel;
using System.Linq;

[Serializable]
public class Stat
{
    public delegate void OnValueChangedDelegate(float oldValue, float newValue);
    public event OnValueChangedDelegate OnValueChanged;
    public ObservableCollection<float> summingMultiplers = new ObservableCollection<float>(new float[1]{1}),
        multiplingMultiplers = new(),
        additions = new();
    private float baseValue = 1, lastValue = 1;
    public Stat(float baseValue)
    {
        this.BaseValue = baseValue;
        summingMultiplers.CollectionChanged += (s,e) => AnyCollectionChanged();
        multiplingMultiplers.CollectionChanged += (s,e) => AnyCollectionChanged();
        additions.CollectionChanged += (s,e)=>AnyCollectionChanged();
    }
    public void AnyCollectionChanged()
    {
        if(lastValue == Value) return;
        OnValueChanged(lastValue,Value);
        lastValue = Value;
    }
    public float multiplingMultiplersResult
    {
        get
        {
            switch (multiplingMultiplers.Count)
            {
                case 0:
                    return 1;
                case 1:
                    return multiplingMultiplers[0];
                default:
                    return multiplingMultiplers.Aggregate((x, y) => x * y);
            }
        }
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
            OnValueChanged(baseValue, value);
            lastValue = Value;
            baseValue = value;
        }
    }

    public static implicit operator int(Stat stat)=> (int) stat.Value;
    public static implicit operator float(Stat stat) => stat.Value;
    public override string ToString()=>$"Value = {Value}, Base Value = {BaseValue}, Sum of multiplers = {summingMultiplers.Sum()}, Sum of additions = {additions.Sum()}";
}