using UnityEditor;
using UnityEngine;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
namespace DustyStudios
{
    public class DustyConsole : EditorWindow
    {
        string command = "";
        [MenuItem("Window/𝗗𝘂𝘀𝘁𝘆 𝗖𝗼𝗻𝘀𝗼𝗹𝗲")]
        public static void ShowWindow()
        {
            GetWindow<DustyConsole>("Dusty Console");
        }

        private void OnGUI()
        {
            GUILayout.Label("Enter Command:");
            command = GUILayout.TextField(command);

            if(GUILayout.Button("Execute"))
            {
                string[] parts = command.Split(' ');
                string commandName = parts[0];
                string[] arguments = parts.Skip(1).ToArray();
                ExecuteCommand(commandName,arguments);
                command = "";
            }
        }
        public void ExecuteCommand(string commandName,string[] arguments)
        {
            object[] resultArray = ConvertArray(arguments);
            MethodInfo[] methods = GetCommands();
            foreach(MethodInfo method in methods)
            {
                DustyConsoleCommandAttribute commandAttribute = (DustyConsoleCommandAttribute)Attribute.GetCustomAttribute(method, typeof(DustyConsoleCommandAttribute));
                if(commandAttribute.Keyword == commandName)
                {
                    method.Invoke(null,resultArray);
                }
            }

        }
        static MethodInfo[] GetCommands()
        {
            List<MethodInfo> commands = new();
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            foreach(var type in types)
            {
                MethodInfo[] thatTypeMethods = type.GetMethods(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                foreach (var method in thatTypeMethods)
                {
                    DustyConsoleCommandAttribute commandAttribute = (DustyConsoleCommandAttribute)Attribute.GetCustomAttribute(method, typeof(DustyConsoleCommandAttribute));
                    if(commandAttribute != null)
                    commands.Add(method);
                }
            }
            return commands.ToArray();
        }
        static object[] ConvertArray(string[] input)
        {
            object[] result = new object[input.Length];

            for(int i = 0; i < input.Length; i++)
            {
                if(int.TryParse(input[i],out int intValue))
                {
                    result[i] = intValue;
                }
                else if(float.TryParse(input[i],out float floatValue))
                {
                    result[i] = floatValue;
                }
                else
                {
                    result[i] = input[i];
                }
            }
            return result;
        }
        [DustyConsoleCommand("help","Show all possible commands")]
        static void Help()
        {
            foreach(MethodInfo method in GetCommands())
            {
                DustyConsoleCommandAttribute commandAttribute = (DustyConsoleCommandAttribute)Attribute.GetCustomAttribute(method, typeof(DustyConsoleCommandAttribute));
                Debug.Log("- "+ commandAttribute.Keyword + " - " + commandAttribute.Description);
            }
        }
    }
    [AttributeUsage(AttributeTargets.Method,AllowMultiple = false,Inherited = false)]
    public class DustyConsoleCommandAttribute : Attribute
    {
        public readonly string Keyword, Description;
        public readonly Type[] Arguments;
        public DustyConsoleCommandAttribute(string keyword,string description,params Type[] args)
        {
            Keyword = keyword;
            Description = description;
            Arguments = args;
        }
    }
}