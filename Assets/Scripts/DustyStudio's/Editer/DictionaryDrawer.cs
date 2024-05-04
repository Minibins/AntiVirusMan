using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using DustyStudios;

[CustomPropertyDrawer(typeof(Dictionary<,>))]
public class DictionaryPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position,SerializedProperty property,GUIContent label)
    {
        DustyConsole.Print(label.text);
        EditorGUI.BeginProperty(position,label,property);

        EditorGUI.LabelField(position,label);

        EditorGUI.indentLevel++;

        // Get the dictionary field
        SerializedProperty dictionary = property.FindPropertyRelative("dictionary");

        // Display each key-value pair
        foreach(SerializedProperty item in dictionary)
        {
            Rect keyRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width / 2f, EditorGUIUtility.singleLineHeight);
            Rect valueRect = new Rect(position.x + position.width / 2f, position.y + EditorGUIUtility.singleLineHeight, position.width / 2f, EditorGUIUtility.singleLineHeight);

            SerializedProperty key = item.FindPropertyRelative("Key");
            SerializedProperty value = item.FindPropertyRelative("Value");

            EditorGUI.PropertyField(keyRect,key,GUIContent.none);
            EditorGUI.PropertyField(valueRect,value,GUIContent.none);

            position.y += EditorGUIUtility.singleLineHeight;
        }

        EditorGUI.indentLevel--;

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property,GUIContent label)
    {
        SerializedProperty dictionary = property.FindPropertyRelative("dictionary");
        return (dictionary.arraySize + 1) * EditorGUIUtility.singleLineHeight;
    }
}
