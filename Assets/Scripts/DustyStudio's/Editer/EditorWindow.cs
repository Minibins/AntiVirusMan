#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace DustyStudios
{
    public class DustyConsoleWindow : EditorWindow
    {
        private string command = "";

        private void Awake()
        {
            DustyConsole.Print("Dusty Console ready for work （￣︶￣）↗");
        }

        [MenuItem("Window/𝗗𝘂𝘀𝘁𝘆 𝗖𝗼𝗻𝘀𝗼𝗹𝗲")]
        public static void ShowWindow()
        {
            GetWindow<DustyConsoleWindow>("Dusty Console");
        }

        private void OnGUI()
        {
            GUILayout.Label("Enter Command:");
            command = GUILayout.TextField(command);

            if (GUILayout.Button("Execute"))
            {
                DustyConsole.ExecuteCommandWithString(command);
            }

            float otherElementsYsize =
                (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 3;
            EditorGUI.TextArea(new Rect(0, otherElementsYsize, position.width, position.height - otherElementsYsize),
                DustyConsole.Output);
            DustyConsole.numberOfOutputInScreen =
                (int) ((position.size.y - otherElementsYsize) / EditorGUIUtility.singleLineHeight);
        }
    }
}
#endif