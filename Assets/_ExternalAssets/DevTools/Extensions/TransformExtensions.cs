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
			if (t == null) return;

			t.localPosition = Vector3.zero;
			t.localRotation = Quaternion.identity;
			t.localScale = Vector3.one;
		}

		/// <summary>
		/// Resets position and rotation in World Space, sets localScale to one.
		/// <br>Note: Global scale (lossyScale) will still inherit from parent objects if any.</br>
		/// </summary>
		public static void ResetWorld(this Transform t)
		{
			if (t == null) return;

			t.position = Vector3.zero;
			t.rotation = Quaternion.identity;
			t.localScale = Vector3.one;
		}
	}
}
