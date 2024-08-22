using UnityEditor;

using UnityEngine;

public class RevertRectTransformOverrides : MonoBehaviour
{
    [MenuItem("Tools/Revert RectTransform Overrides in Selected")]
    static void RevertOverridesInSelected()
    {
        // Получаем текущий выбранный объект
        GameObject selectedObject = Selection.activeGameObject;

        if(selectedObject == null)
        {
            Debug.LogWarning("Please select a GameObject in the scene.");
            return;
        }

        // Получаем все RectTransform компоненты в выбранном объекте и его потомках
        var rectTransforms = selectedObject.GetComponentsInChildren<RectTransform>();

        foreach(var rectTransform in rectTransforms)
        {
            var prefabAsset = PrefabUtility.GetCorrespondingObjectFromSource(rectTransform.gameObject);

            if(prefabAsset != null)
                foreach(string str in new[] { "m_AnchoredPosition","m_AnchorMin","m_AnchorMax","m_SizeDelta","m_Pivot" })
                    PrefabUtility.RevertPropertyOverride(new SerializedObject(rectTransform).FindProperty(str),InteractionMode.UserAction);
        }

        Debug.Log("RectTransform overrides reverted for selected GameObject and its children.");
    }
}
