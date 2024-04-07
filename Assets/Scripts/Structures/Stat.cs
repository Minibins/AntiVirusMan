using System.Collections.Generic;
using System.Linq;
using System;
[System.Serializable]
public class Stat
{
    public List<float> summingMultiplers=new(new float[1]{1}), multiplingMultiplers = new(), additions=new();
    public float baseValue = 1;

    public Stat(float baseValue)
    {
        this.baseValue = baseValue;
    }
    public float multiplingMultiplersResult
    {
        get
        {
            switch(multiplingMultiplers.Count)
            {
                case 0:
                return 1;
                case 1:
                return multiplingMultiplers[0];
                default:
                return multiplingMultiplers.Aggregate((x,y) => x * y);
            }
        }
    }
    public float Value
    {
        get => (baseValue * Math.Max(summingMultiplers.Sum(),1) + additions.Sum())* multiplingMultiplersResult;
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
        return $"Value = {Value}, Base Value = {baseValue}, Sum of multiplers = {summingMultiplers.Sum()}, Sum of additions = {additions.Sum()}";
    }
}
