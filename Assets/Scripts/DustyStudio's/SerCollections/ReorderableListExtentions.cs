using System.Collections;
using System.Collections.Generic;

using UnityEditorInternal;
using UnityEngine;

public static class ReorderableListExtentions
{
    public static IEnumerator GetEnumerator(this ReorderableList list)
    {
        if(list.list != null)
        {
            foreach(var item in list.list)
                yield return item;
        }
        else
        {
            int size = list.serializedProperty.arraySize;
            for(int i = 0; i < size;)
                yield return list.serializedProperty.GetArrayElementAtIndex(i++);
        }
    }
    public static List<object> GetList(this ReorderableList list)
    {
        return list.GetEnumerator().ToList();
    }
}
