using UnityEngine;

namespace DataExtensions
{
	public static class DataExtension
	{
		public static float SqrMagnitudeTo(this Vector3 from, Vector3 to)
		{
			return Vector3.SqrMagnitude(to - from);
		}
	}
}