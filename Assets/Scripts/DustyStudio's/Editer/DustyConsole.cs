using UnityEditor;
using UnityEngine;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEditor.Rendering;
using Unity.VisualScripting;
namespace DustyStudios
{
    public class DustyConsole
    {
        static List<string> output = new();
        public static string Output
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
        public static int numberOfOutputInScreen = 6;
        
        
        public static event Action<string> onPrint;
        public static void Print(object line)
        {
            string Line = line.ToString();
            output.Add(Line);
            onPrint(Line);
        }
        public static void ExecuteCommandWithString(string command)
        {
            string[] parts = command.Split(' ');
            string commandName = parts[0];
            string[] arguments = parts.Skip(1).ToArray();
            ExecuteCommand(commandName,arguments);
        }
        public static void LogCallback(string message,string stackTrace,LogType type)
        {
            string symbol = "";
            switch(type)
            {
                case LogType.Error:
                    symbol = "!!"; break;
                case LogType.Log:
                    symbol = "i"; break;
                case LogType.Assert:
                    symbol = "I"; break;
                case LogType.Warning:
                    symbol = "!"; break;
                case LogType.Exception:
                    symbol = "!!!"; break;
            }
            Print(symbol + ": " + message);
        }
        public static void ExecuteCommand(string commandName,string[] arguments)
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
                            Print(method.Invoke(null,resultArray).ToString());
                            return;
                        }
                    }
                }
            }

            Print("Invalid command or arguments.");
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