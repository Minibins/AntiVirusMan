#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Reflection;
using DustyStudios.SerCollections;
using System.Collections.Specialized;
using System.ComponentModel;
using System;

[CustomPropertyDrawer(typeof(DustyStudios.SerCollections.SerObservableCollection<>),true)]
public class SerializableObservableCollectionEditor : PropertyDrawer
{
    private ReorderableList reorderableList;
    private int size;
    public override void OnGUI(Rect position,SerializedProperty property,GUIContent label)
    {
        #region сериализация
        EditorGUI.BeginProperty(position,label,property);
        if(reorderableList == null)
            InitializeReorderableList(property,label);
        #endregion
        if(property.isExpanded)
            reorderableList.DoList(new(position.x,position.y,position.width,position.height));
        else
            property.isExpanded = EditorGUI.Foldout(new(position.x,position.y,position.width,EditorGUIUtility.singleLineHeight),property.isExpanded,label.text);
        int newSize;
        newSize = EditorGUI.IntField(new Rect(EditorGUIUtility.currentViewWidth - EditorGUIUtility.fieldWidth,position.y,EditorGUIUtility.fieldWidth,EditorGUIUtility.singleLineHeight),size);
        if(newSize!=size)
        {
            while(size > newSize) RemoveElementFromList(reorderableList);
            while(size < newSize) AddElementToList(reorderableList);
        }
        EditorGUI.EndProperty();
    }
    private void InitializeReorderableList(SerializedProperty property, GUIContent label)
    {
        SerializedProperty items = property.FindPropertyRelative("itemsList");
        reorderableList = new(property.serializedObject,items,true,true,true,true);
        reorderableList.drawHeaderCallback = rect => property.isExpanded = EditorGUI.Foldout(rect,property.isExpanded,label);
        reorderableList.drawElementCallback = (rect,index,isActive,isFocused) =>
        {
            var element = items.GetArrayElementAtIndex(index);
            rect.height -= EditorGUIUtility.standardVerticalSpacing*2;
            rect.y += EditorGUIUtility.standardVerticalSpacing;
            EditorGUI.PropertyField(rect,element,GUIContent.none);
        };

        reorderableList.onAddCallback = list => AddElementToList(list);
        /* {
           var index = list.serializedProperty.arraySize;
            list.serializedProperty.arraySize++;
            list.index = index;

            var element = list.serializedProperty.GetArrayElementAtIndex(index);

            switch(element.propertyType)
            {
                case SerializedPropertyType.Integer or SerializedPropertyType.LayerMask:
                element.intValue = 0;
                break;
                case SerializedPropertyType.Float:
                element.floatValue = 0;
                break;
                case SerializedPropertyType.Enum:
                element.enumValueIndex = 0;
                break;
                case SerializedPropertyType.Boolean:
                element.boolValue = false; break;
                case SerializedPropertyType.String:
                element.stringValue = "";
                break;
                case SerializedPropertyType.Vector2:
                element.vector2Value = Vector2.zero;
                break;
                default:
                element.managedReferenceValue = null;
                break;
            };
            list.serializedProperty.serializedObject.ApplyModifiedProperties();
        };*/
        reorderableList.onRemoveCallback = list => RemoveElementFromList(list);
        size = reorderableList.count;
    }
    private void AddElementToList(ReorderableList list)
    {
        ReorderableList.defaultBehaviours.DoAddButton(list);
        size++;
    }
    private void RemoveElementFromList(ReorderableList list)
    {
        ReorderableList.defaultBehaviours.DoRemoveButton(list);
        size--;
    }
    public override float GetPropertyHeight(SerializedProperty property,GUIContent label) => property.isExpanded?(reorderableList != null ? reorderableList.GetHeight() : base.GetPropertyHeight(property,label)): EditorGUIUtility.singleLineHeight;

}
#endif