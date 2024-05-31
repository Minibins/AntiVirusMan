using DustyStudios.MathAVM;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Unity.VisualScripting;

using UnityEngine;

namespace DustyStudios
{
    public class DustyConsole
    {
        private static Queue<string> output = new();

        public static string Output
        {
            get
            {
                List<string> list = output.ToList();
                string o = "";
                for (int i = output.Count; i > 0;)
                {
                    o += list.ToList()[--i];
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
            output.Enqueue(Line);
            if(output.Count>numberOfOutputInScreen) output.Dequeue();
            onPrint?.Invoke(Line);
        }

        public static void ExecuteCommandWithString(string command)
        {
            string[] parts = command.Split(' ');
            string commandName = parts[0];
            string[] arguments = parts.Skip(1).ToArray();
            ExecuteCommand(commandName, arguments);
        }

        public static void LogCallback(string message, string stackTrace, LogType type)
        {
            string symbol = "";
            switch (type)
            {
                case LogType.Error:
                    symbol = "!!";
                    break;
                case LogType.Log:
                    symbol = "i";
                    break;
                case LogType.Assert:
                    symbol = "I";
                    break;
                case LogType.Warning:
                    symbol = "!";
                    break;
                case LogType.Exception:
                    symbol = "!!!";
                    Match match = Regex.Match(stackTrace, @"\((at <[^)]+>:0)\)");
                    if (match.Success) symbol += '\n' + match.Groups[1].Value + '\n';
                    break;
            }

            Print(symbol + ": " + message);
        }

        public static void ExecuteCommand(string commandName, string[] arguments)
        {
            MethodInfo[] methods = GetCommands();
            foreach (MethodInfo method in methods)
            {
                DustyConsoleCommandAttribute commandAttribute =
                    (DustyConsoleCommandAttribute) Attribute.GetCustomAttribute(method,
                        typeof(DustyConsoleCommandAttribute));
                if (commandAttribute.Keyword == commandName)
                {
                    ParameterInfo[] parameters = method.GetParameters();
                    if (parameters.Length == arguments.Length)
                    {
                        object[] resultArray = ConvertArray(arguments,method.GetParameters().Select(p => p.ParameterType).ToArray());
                        Print(method.Invoke(null, resultArray).ToString());
                        return;
                    }
                }
            }
            Print("Invalid command or arguments!");
        }
        private static MethodInfo[] GetCommands()
        {
            List<MethodInfo> commands = new List<MethodInfo>();
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                MethodInfo[] thatTypeMethods = type.GetMethods(BindingFlags.Static | BindingFlags.Instance |
                                                               BindingFlags.Public | BindingFlags.NonPublic);
                foreach (var method in thatTypeMethods)
                {
                    DustyConsoleCommandAttribute commandAttribute =
                        (DustyConsoleCommandAttribute) Attribute.GetCustomAttribute(method,typeof(DustyConsoleCommandAttribute));
                    if (commandAttribute != null)
                        commands.Add(method);
                }
            }

            return commands.ToArray();
        }
        private static object[] ConvertArray(string[] input,Type[] types)
        {
            if(input.Length != types.Length) Print($"Invalid number of arguments: your input has {input.Length} but should {types.Length}");
            Dictionary<Type,Func<string,object>> TypeConverters = new Dictionary<Type,Func<string,object>>
            {
                { typeof(int), HandleInt },
                { typeof(float), HandleFloat },
                { typeof(double), HandleDouble },
                { typeof(string), HandleString },
            };
            static object HandleInt(string s) => int.Parse(s);
            static object HandleFloat(string s)
            {
                if(MathA.TryStringToNumber(s,out double floatValue)) return (float)floatValue;
                else
                {
                    Print("! String to decimal conversion error! Command invokes with \"0\" argument");
                    return 0f;
                } 
            }
            static object HandleDouble(string s)
            {
                if(MathA.TryStringToNumber(s,out double doubleValue)) return doubleValue;
                else
                {
                    Print("! String to decimal conversion error! Command invokes with \"0\" argument");
                    return 0d;
                } 
            }
            static object HandleString(string s) => s;
            object[] result = new object[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                try
                {
                    if(TypeConverters.ContainsKey(types[i])) result[i] = TypeConverters[types[i]](input[i]);
                    else
                    {
                        Print("! Console no longer support that command, because it has argument of type:" + types[i].Name);
                        result[i] = input;
                    }
                }
                catch(Exception e)
                {
                    Print($"Your input ({input[i]}) just caused error: {e.Message}");
                }
            }
            return result;
        }

        [DustyConsoleCommand("help","Show all possible commands")]
        private static string Help()
        {
            string commands = "";
            MethodInfo[] methods = GetCommands();
            for(int i = 0; i < Math.Min(methods.Length,numberOfOutputInScreen);)
            {
                var method = methods[i++];
                var commandAttribute = (DustyConsoleCommandAttribute) Attribute.GetCustomAttribute(method, typeof(DustyConsoleCommandAttribute));
                string argumentTypes = string.Join(", ", commandAttribute.Arguments.Select(arg => TypeNames.TryGetValue(arg, out var name) ? name : arg.Name));
                string desc = $"- {commandAttribute.Keyword} {argumentTypes} - {commandAttribute.Description}\n";
                if(commands.Contains(desc)) numberOfOutputInScreen++;
                else commands += desc;
            }
            commands += "Execute help [Page], to get next pages";
            return commands;
        }
        private static readonly Dictionary<Type, string> TypeNames = new Dictionary<Type, string>
        {
        { typeof(short), "int" },
        { typeof(int), "int" },
        { typeof(long), "int" },
        { typeof(float), "decimal" },
        { typeof(double), "decimal" },
        { typeof(bool), "boolean" },
        { typeof(string), "word" },
        };


        [DustyConsoleCommand("help", "Show all possible commands")]
        private static string Help(int page)
        {
            if (page < 0) return "Page doesnt exist";
            page *= numberOfOutputInScreen;
            string commands = "";
            MethodInfo[] methods = GetCommands();
            for (int i = page; i < Math.Min(methods.Length, numberOfOutputInScreen + page);)
            {
                DustyConsoleCommandAttribute commandAttribute =
                    (DustyConsoleCommandAttribute) Attribute.GetCustomAttribute(methods[i++],
                        typeof(DustyConsoleCommandAttribute));
                string argumentTypes = string.Join(", ", commandAttribute.Arguments.Select(arg => TypeNames.TryGetValue(arg, out var name) ? name : arg.Name));
                string desc = $"- {commandAttribute.Keyword} {argumentTypes} - {commandAttribute.Description}\n";
                if (commands.Contains(desc)) numberOfOutputInScreen++;
                else commands += desc;
            }

            if (commands.Length == 0) return "Page doesnt exist";
            return commands;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class DustyConsoleCommandAttribute : Attribute
    {
        public readonly string Keyword, Description;
        public readonly Type[] Arguments;

        public DustyConsoleCommandAttribute(string keyword, string description, params Type[] args)
        {
            Keyword = keyword;
            Description = description;
            Arguments = args;
        }
    }
}