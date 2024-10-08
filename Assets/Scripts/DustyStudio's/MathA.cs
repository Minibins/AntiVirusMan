using System;
using System.Linq;
using UnityEngine;

namespace DustyStudios
{
    namespace MathAVM
    {
        public static class MathA
        {
            public enum roundMode
            {
                min, normal
            }
            public static Vector3Int Round(this Vector3 vector, roundMode mode)
            {
                return new Vector3Int(
                    RoundedValue(vector.x),
                    RoundedValue(vector.y),
                    RoundedValue(vector.z)
                );
                int RoundedValue(float value)
                {
                    switch (mode)
                    {
                        case roundMode.min: return (int)value;
                        default: return Mathf.RoundToInt(value);
                    }
                }
            }
            public static Vector3 Clamp(this Vector3 vector, float min, float max) =>
                new(
                    Mathf.Clamp(vector.x,min,max),
                    Mathf.Clamp(vector.y,min,max),
                    Mathf.Clamp(vector.z,min,max)
                );
            public static Vector3 NormalizedMin1(this Vector3 vector)
            {
                vector.Normalize();
                float minValue = Math.Abs(vector.x);
                SetToMinAndNot0(vector.y);
                SetToMinAndNot0(vector.z);
                void SetToMinAndNot0(float second)
                {
                    if(Math.Abs(second) > minValue && second != 0)
                        minValue = second;
                }
                vector /= Math.Abs(minValue);
                return vector;
            }
            private static Vector2Int[] OrderByMagnitude(params Vector2Int[] args)
            {
                return args.OrderByDescending(arg => arg.magnitude).ToArray();
            }

            public static Vector2Int Max(params Vector2Int[] args)
            {
                return OrderByMagnitude(args)[0];
            }

            public static Vector2Int Min(params Vector2Int[] args)
            {
                return OrderByMagnitude(args).Last();
            }

            private static Vector2[] OrderByMagnitude(params Vector2[] args)
            {
                return args.OrderByDescending(arg => arg.magnitude).ToArray();
            }

            public static Vector2 Max(params Vector2[] args)
            {
                return OrderByMagnitude(args)[0];
            }

            public static Vector2 Min(params Vector2[] args)
            {
                return OrderByMagnitude(args).Last();
            }

            private static Vector3[] OrderByMagnitude(params Vector3[] args)
            {
                return args.OrderByDescending(arg => arg.magnitude).ToArray();
            }

            public static Vector3 Max(params Vector3[] args)
            {
                return OrderByMagnitude(args)[0];
            }

            public static Vector3 Min(params Vector3[] args)
            {
                return OrderByMagnitude(args).Last();
            }

            public static Vector2 RotatedVector(Vector2 VectorToRotate, float angle)
            {
                float cosin = angle * Mathf.Deg2Rad;
                float sinus = Mathf.Sin(cosin);
                cosin = Mathf.Cos(cosin);
                Vector2 result = new Vector2(
                    VectorToRotate.x * cosin - VectorToRotate.y * sinus,
                    VectorToRotate.x * sinus + VectorToRotate.y * cosin
                );
                return result;
            }


            public static Vector2 RotatedVector(Vector2 VectorToRotate, Vector2 angleExample)
            {
                return angleExample.normalized * VectorToRotate.magnitude;
            }

            public static Vector3 Multiply(this Vector3 vector, Vector3 vector2)
            {
                return new Vector3(vector.x * vector2.x, vector.y * vector2.y, vector.z * vector2.z);
            }

            public static Vector3 Delit(this Vector3 vector, Vector3 vector2)
            {
                return new Vector3(vector.x / vector2.x, vector.y / vector2.y, vector.z / vector2.z);
            }

            public static Vector3 VozvestiVstepen(this Vector3 vector, int stepen)
            {
                return new Vector3((float) Math.Pow((double) vector.x, (double) stepen),
                    (float) Math.Pow((double) vector.y, (double) stepen),
                    (float) Math.Pow((double) vector.z, (double) stepen));
            }

            public static Quaternion VectorsAngle(Vector2 vector)
            {
                return Quaternion.Euler(0, 0, Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg);
            }


            public static Color ColorBetweenTwoOther(Color first, Color second, int startOfTransformation,
                int endOfTransformation, int proportion)
            {
                proportion = Mathf.Max(startOfTransformation, Mathf.Min(endOfTransformation, proportion));

                float factor = (float) (proportion - startOfTransformation) /
                               (endOfTransformation - startOfTransformation);

                float red = (first.r * (1 - factor) + second.r * factor);
                float green = (first.g * (1 - factor) + second.g * factor);
                float blue = (first.b * (1 - factor) + second.b * factor);
                float alpha = (first.a * (1 - factor) + second.a * factor);

                return new Color(red, green, blue, alpha);
            }

            public static sbyte OneOrNegativeOne(float number)
            {
                return number >= 0 ? (sbyte) 1 : (sbyte) -1;
            }

            public static sbyte OneOrNegativeOne(bool boolean)
            {
                return !boolean ? (sbyte) 1 : (sbyte) -1;
            }
            public const double GoldenRatio = 1.61803398874989484820458683436563811772030917980576286213544862270526046281890;

            public static readonly (string symbol, double value)[] constants = new[] { 
                ("π", Math.PI), 
                ("φ", GoldenRatio), 
                ("e", Math.E) 
            };
            [DustyConsoleCommand("number","I just want you to look at my system for converting numbers to text ^υ^",typeof(double))]
            public static string NumberToString(double number)
            {
                const float tolerance = 0.05f;
                if(number==0)                             return "0";
                else if(double.IsNaN(number))             return "?";
                else if(number == double.PositiveInfinity)return "∞";
                else if(number == double.NegativeInfinity)return "-∞";
                else if(number == float.Epsilon)          return "ε";
                else if(number == -float.Epsilon)         return "-ε";
                else if(number == double.MaxValue || number == float.MaxValue || number == int.MaxValue || number == long.MaxValue) return "Gazillion";
                else if(number == double.MinValue || number == float.MinValue || number == int.MinValue || number == long.MinValue) return "-Gazillion";
                else
                {
                    string multiplierConsts = "";

                    foreach(var constant in constants)
                        ParseConstant(constant.symbol, constant.value);
                    void ParseConstant(string symbol, double value)
                    {
                        if(Math.Abs(number) % value <= tolerance)
                        {
                            number /= value;
                            multiplierConsts += symbol;
                        }
                    }
                    string Number = number.ToString().Take(5).Aggregate("",(x,y) => (string)x+y);
                    return Number + multiplierConsts;
                }
            }
            public static readonly string[] MaxValueNames = { "Google", "MaxValue","Gazillion", "Morbillion", "Kajillion", "Bajillion", "Squillion", "Zinglezangillion", "Baggillion", "Zentillion", "Дофигаллион", "Морбиллион", "Дохуяллион", "Хрензнаетскольколлион", "Максимальное число","Многа" };
            public static bool TryStringToNumber(string str, out double number)
            {
                number = 666;
                str = str.Replace('.',',');
                if(str == "?") number = double.NaN;
                else if(str == "∞") number = double.PositiveInfinity;
                else if(str == "-∞") number = double.NegativeInfinity;
                else if(str == "ε") number = float.Epsilon;
                else if(str == "-ε") number = -float.Epsilon;
                else if(MaxValueNames.Any(n => n.ToLower() == str.ToLower())) number = double.MaxValue;
                else if(MaxValueNames.Any(n => "-" + n.ToLower() == str.ToLower())) number = double.MinValue;
                else if(str.EndsWith("π")) { string s = str.Replace("π",""); number = double.Parse((s.StartsWith("-") ? "-" : "") + (s.Replace("-","") != "" ? s.Replace("-","") : "1")) * Mathf.PI; }
                else if(str.EndsWith("φ")) { string s = str.Replace("φ",""); number = double.Parse((s.StartsWith("-") ? "-" : "") + (s.Replace("-","") != "" ? s.Replace("-","") : "1")) * GoldenRatio; }
                else if(str.EndsWith("e")) { string s = str.Replace("e",""); number = double.Parse((s.StartsWith("-") ? "-" : "") + (s.Replace("-","") != "" ? s.Replace("-","") : "1")) * Math.E; }
                else if(double.TryParse(str,out double d)) number = d;
                else return false;
                    return true;
                }
        }

        [Serializable]
        public class SinusWave
        {
            [SerializeField] public float Amplitude, Length, CurrentPos;

            public float Value
            {
                get { return ValueIn(CurrentPos); }
            }

            public float ValueIn(float Pos)
            {
                return Mathf.Sin(Pos * Length) * Amplitude;
            }
        }
    }
}