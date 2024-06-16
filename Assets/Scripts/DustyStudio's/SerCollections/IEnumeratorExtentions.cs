using System.Collections;
using System.Collections.Generic;
public static class IEnumeratorExtentions
{
    public static List<object> ToList(this IEnumerator enumerator)
    {
        List<object> list = new();
        while(enumerator.MoveNext())
            list.Add(enumerator.Current);
        return list;
    }
}
