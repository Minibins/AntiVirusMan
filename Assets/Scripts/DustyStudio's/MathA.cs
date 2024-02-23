using System.Linq;

using UnityEngine;
namespace DustyStudios
{
    namespace MathAVM
    {
        public struct MathA
        {
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
            public static Vector2 RotatedVector(Vector2 VectorToRotate,float angle)
            {
                float rotationInRadians=angle*Mathf.PI/180;
                Vector2 result = new Vector2(VectorToRotate.x * Mathf.Cos(rotationInRadians) - VectorToRotate.y * Mathf.Sin(rotationInRadians),
                                                            VectorToRotate.x * Mathf.Sin(rotationInRadians) + VectorToRotate.y * Mathf.Cos(rotationInRadians)
                                                             );
                return result;
            }


            public static Vector2 RotatedVector(Vector2 VectorToRotate,Vector2 angleExample)
            {
                return angleExample.normalized * VectorToRotate.magnitude;
            }


            public static Quaternion VectorsAngle(Vector2 vector)
            {
                return Quaternion.Euler(0,0,Mathf.Atan2(vector.y,vector.x)*Mathf.Rad2Deg);
            }


            public static Color ColorBetweenTwoOther(Color first,Color second,int startOfTransformation,int endOfTransformation,int proportion)
            {
                proportion = Mathf.Max(startOfTransformation,Mathf.Min(endOfTransformation,proportion));

                float factor = (float)(proportion - startOfTransformation) / (endOfTransformation - startOfTransformation);

                float red = (first.r * (1 - factor) + second.r * factor);
                float green = (first.g * (1 - factor) + second.g * factor);
                float blue = (first.b * (1 - factor) + second.b * factor);
                float alpha = (first.a * (1 - factor) + second.a * factor);

                return new Color(red,green,blue,alpha);
            }

            public static sbyte OneOrNegativeOne(float number)
            {
                return number >= 0 ? (sbyte)1 : (sbyte)-1;
            }
            public static sbyte OneOrNegativeOne(bool boolean)
            {
                return !boolean ? (sbyte)1 : (sbyte)-1;
            }
        }
        [System.Serializable]
        public class SinusWave
        {
            [SerializeField] public float Amplitude, Length, CurrentPos;
            public float Value
            { get { return ValueIn(CurrentPos); } }

            public float ValueIn(float Pos)
            {
                return Mathf.Sin(Pos * Length) * Amplitude;
            }
        }
    }

}


