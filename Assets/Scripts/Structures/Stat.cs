using System.Collections.Generic;
using System.Linq;
[System.Serializable]
public class Stat
{
    public List<float> multiplers=new(), additions=new();
    public float baseValue;

    public float Value
    {
        get => baseValue * (multiplers.Sum()+1) + additions.Sum();
    }
    public static implicit operator int(Stat stat)
    {
        return (int)stat.Value;
    }
    public static implicit operator float(Stat stat)
    {
        return stat.Value;
    }
}
