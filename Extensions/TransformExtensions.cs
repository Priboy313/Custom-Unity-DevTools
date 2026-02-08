using UnityEngine;

namespace DevTools.Extensions
{
	public static class TransformExtensions
	{
		/// <summary>
		/// Resets localPosition, localRotation, and localScale to default values.
		/// <br>Matches the Unity Inspector "Reset" functionality.</br>
		/// </summary>
		public static void Reset(this Transform t)
		{
			t.localPosition = Vector3.zero;
			t.localRotation = Quaternion.identity;
			t.localScale = Vector3.one;
		}

		/// <summary>
		/// Resets position and rotation in World Space, sets scale to one.
		/// </summary>
		public static void ResetWorld(this Transform t)
		{
			t.position = Vector3.zero;
			t.rotation = Quaternion.identity;
			t.localScale = Vector3.one;
		}
	}
}
