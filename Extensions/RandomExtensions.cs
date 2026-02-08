using UnityEngine;
using Random = UnityEngine.Random;

namespace DevTools.Extensions
{
	public static class RandomExtensions
	{
		private const float ChanceMax = 100f;

		#region Range Extensions

		/// <summary>
		/// Returns a random integer between min and max.
		/// <br>Automatically swaps min and max if they are in the wrong order.</br>
		/// <br>Example: <code>5.RandomTo(10)</code></br>
		/// </summary>
		public static int RandomTo(this int min, int max)
		{
			return Random.Range(Mathf.Min(min, max), Mathf.Max(min, max));
		}

		/// <summary>
		/// Returns a random float between min and max.
		/// <br>Automatically swaps min and max if they are in the wrong order.</br>
		/// <br>Example: <code>4.5f.RandomTo(17.3f)</code></br>
		/// </summary>
		public static float RandomTo(this float min, float max)
		{
			return Random.Range(Mathf.Min(min, max), Mathf.Max(min, max));
		}

		/// <summary>
		/// Returns a random vector with each component between the corresponding components of min and max.
		/// <br>Example: <code>startPos.RandomTo(endPos)</code></br>
		/// </summary>
		public static Vector3 RandomTo(this Vector3 min, Vector3 max)
		{
			return new Vector3(
				Random.Range(min.x, max.x),
				Random.Range(min.y, max.y),
				Random.Range(min.z, max.z)
			);
		}

		/// <summary>
		/// Returns a random point inside the given bounds.
		/// <br>Useful for spawning inside a BoxCollider.</br>
		/// <br>Example: <code>myCollider.bounds.GetRandomPoint()</code></br>
		/// </summary>
		public static Vector3 GetRandomPoint(this Bounds bounds)
		{
			return new Vector3(
				Random.Range(bounds.min.x, bounds.max.x),
				Random.Range(bounds.min.y, bounds.max.y),
				Random.Range(bounds.min.z, bounds.max.z)
			);
		}

		#endregion

		#region Probability Extensions

		/// <summary>
		/// Returns true with the specified probability (in percent).
		/// <br>Example: <code>25.TryChance() // 25% chance of success</code></br>
		/// </summary>
		public static bool TryChance(this int percent)
		{
			return Random.value < (percent / ChanceMax);
		}

		/// <summary>
		/// Returns true with the specified probability (in percent).
		/// <br>Example: <code>0.5f.TryChance() // 0.5% chance of success</code></br>
		/// </summary>
		public static bool TryChance(this float percent)
		{
			return Random.value < (percent / ChanceMax);
		}

		#endregion

		#region Vector Conversations

		/// <summary>
		/// Returns a random normalized vector on the XZ plane (Y = 0).
		/// <br>Note: In C#, extension methods require an instance, so we call it from any vector (usually Vector3.zero).</br>
		/// <br>Example: <code>Vector3.zero.RandomXZ()</code></br>
		/// </summary>
		public static Vector3 RandomXZ(this Vector3 _)
		{
			return Random.insideUnitCircle.ToVector3XZ();
		}

		/// <summary>
		/// Converts a Vector2 (X, Y) to a Vector3 (X, 0, Y).
		/// <br>Perfect for converting Random.insideUnitCircle to ground plane coordinates.</br>
		/// <br>Example: <code>Random.insideUnitCircle.ToVector3XZ()</code></br>
		/// </summary>
		public static Vector3 ToVector3XZ(this Vector2 vector)
		{
			return new Vector3(vector.x, 0, vector.y);
		}

		#endregion
	}
}