using UnityEngine;

namespace DustyStudios.MathAVM
{
	public static class Vector3Extentions
	{
		public static Vector3Int Round(this Vector3 vector, RoundMode mode)
		{
			return new Vector3Int(
				RoundedValue(vector.x),
				RoundedValue(vector.y),
				RoundedValue(vector.z)
			);
			int RoundedValue(float value)
			{
				switch(mode)
				{
					case RoundMode.min: return (int)value;
					default: return Mathf.RoundToInt(value);
				}
			}
		}
		public static Vector3 Clamp(this Vector3 vector, float min, float max) =>
			new(
				Mathf.Clamp(vector.x, min, max),
				Mathf.Clamp(vector.y, min, max),
				Mathf.Clamp(vector.z, min, max)
			);
		public static Vector3 NormalizedMin1(this Vector3 vector)
		{
			vector.Normalize();
			if(vector != Vector3.zero)
			{
				float minValue = Mathf.Abs(vector.x);
				SetToMinAndNot0(vector.y);
				SetToMinAndNot0(vector.z);
				void SetToMinAndNot0(float second)
				{
					if(Mathf.Abs(second) > minValue) minValue = second;
				}
				vector /= minValue;
			}
			return vector;
		}

		public static Vector3 Multiply(this Vector3 vector, Vector3 vector2) => new Vector3(vector.x * vector2.x, vector.y * vector2.y, vector.z * vector2.z);
		public static Vector3 Divide(this Vector3 vector, Vector3 vector2) => new Vector3(vector.x / vector2.x, vector.y / vector2.y, vector.z / vector2.z);
		public static Vector3 Pow(this Vector3 vector, int stepen) => new Vector3(Mathf.Pow(vector.x, stepen),Mathf.Pow(vector.y, stepen),Mathf.Pow(vector.z, stepen));
	}
}