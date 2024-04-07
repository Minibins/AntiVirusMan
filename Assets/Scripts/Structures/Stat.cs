using System.Collections.Generic;
using System.Linq;
using System;
[System.Serializable]
public class Stat
{
    public List<float> multiplers=new(new float[1]{1}), additions=new();
    public float baseValue = 1;

    public Stat(float baseValue)
    {
        this.baseValue = baseValue;
    }

    public float Value
    {
        get => baseValue * Math.Max(multiplers.Sum(),1) + additions.Sum();
    }
    public static implicit operator int(Stat stat)
    {
        return (int)stat.Value;
    }
    public static implicit operator float(Stat stat)
    {
        return stat.Value;
    }
    public override string ToString()
    {
        return $"Value = {Value}, Base Value = {baseValue}, Sum of multiplers = {multiplers.Sum()}, Sum of additions = {additions.Sum()}";
    }
}
