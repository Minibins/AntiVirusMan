using UnityEditor;
using UnityEngine;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
namespace DustyStudios
{
    public class DustyConsole : EditorWindow
    {
        string command = "";
        List<string> output = new();
        string Output
        {
            get
            {
                string o = "";
                for(int i = output.Count; i > 0;)
                {
                    o+=(output[--i]);
                    o += "\n";
                }
                return o;
            }
        }
        static int numberOfOutputInScreen;
        [MenuItem("Window/𝗗𝘂𝘀𝘁𝘆 𝗖𝗼𝗻𝘀𝗼𝗹𝗲")]
        public static void ShowWindow()
        {
            GetWindow<DustyConsole>("Dusty Console");
        }
        private void Awake()
        {
            output.Add("Dusty Console ready for work （￣︶￣）↗");
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
            }
            float otherElementsYsize = (EditorGUIUtility.singleLineHeight+EditorGUIUtility.standardVerticalSpacing)*3;
            EditorGUI.TextArea(new Rect(0,otherElementsYsize,position.width,position.height - otherElementsYsize),Output);
            numberOfOutputInScreen = (int)((position.size.y-otherElementsYsize)/ EditorGUIUtility.singleLineHeight);
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
                    ParameterInfo[] parameters = method.GetParameters();

                    if(parameters.Length == arguments.Length)
                    {
                        bool validSignature = true;

                        for(int i = 0; i < parameters.Length; i++)
                        {
                            if(parameters[i].ParameterType != resultArray[i].GetType())
                            {
                                validSignature = false;
                                break;
                            }
                        }

                        if(validSignature)
                        {
                            output.Add(method.Invoke(null,resultArray).ToString());
                            return;
                        }
                    }
                }
            }

            output.Add("Invalid command or arguments.");
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
        static string Help()
        {
            string commands = "";
            MethodInfo[] methods = GetCommands();
            for(int i = 0; i < Math.Min(methods.Length,numberOfOutputInScreen);)
            {
                DustyConsoleCommandAttribute commandAttribute = (DustyConsoleCommandAttribute)Attribute.GetCustomAttribute(methods[i++], typeof(DustyConsoleCommandAttribute));
                string desc = "- " + commandAttribute.Keyword + " - " + commandAttribute.Description + "\n";
                if(commands.Contains(desc)) numberOfOutputInScreen++;
                else commands += desc;
            }
            commands += "Execute help [Page], to get next pages";
            return commands;
        }
        [DustyConsoleCommand("help","Show all possible commands")]
        static string Help(int page)
        {
            if(page < 0) return "Page doesnt exist";
            page *= numberOfOutputInScreen;
            string commands = "";
            MethodInfo[] methods = GetCommands();
            for(int i = page; i < Math.Min(methods.Length,numberOfOutputInScreen+page);)
            {
                DustyConsoleCommandAttribute commandAttribute = (DustyConsoleCommandAttribute)Attribute.GetCustomAttribute(methods[i++], typeof(DustyConsoleCommandAttribute));
                string desc = "- " + commandAttribute.Keyword + " - " + commandAttribute.Description + "\n";
                if(commands.Contains(desc)) numberOfOutputInScreen++;
                else commands += desc;
            }
            if(commands.Length == 0) return "Page doesnt exist"; 
            return commands;
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