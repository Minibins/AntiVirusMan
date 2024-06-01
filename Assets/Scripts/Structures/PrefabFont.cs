using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class PrefabFont : ScriptableObject
{
    public List<LetterToGameobject> List;
    public List<StringToGameobject> ListOfStrings;
    [Serializable]
    public struct LetterToGameobject
    {
        public char letter;
        public RectTransform gameObject;
    }
    [Serializable]
    public struct StringToGameobject
    {
        public string String;
        public RectTransform gameObject;
    }
}