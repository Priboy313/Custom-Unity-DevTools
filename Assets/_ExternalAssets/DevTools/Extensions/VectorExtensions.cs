using UnityEngine;

namespace DevTools.Extensions
{
	public static class VectorExtensions
	{
		/// <summary>
		/// Returns a new Vector3 with the X component modified.
		/// <br>Example: <code>transform.position = transform.position.WithX(5f);</code></br>
		/// </summary>
		public static Vector3 WithX(this Vector3 v, float x)
		{
			return new Vector3(x, v.y, v.z);
		}

		/// <summary>
		/// Returns a new Vector3 with the Y component modified.
		/// <br>Example: <code>transform.position = transform.position.WithY(5f);</code></br>
		/// </summary>
		public static Vector3 WithY(this Vector3 v, float y)
		{
			return new Vector3(v.x, y, v.z);
		}

		/// <summary>
		/// Returns a new Vector3 with the Z component modified.
		/// <br>Example: <code>transform.position = transform.position.WithZ(5f);</code></br>
		/// </summary>
		public static Vector3 WithZ(this Vector3 v, float z)
		{
			return new Vector3(v.x, v.y, z);
		}

		/// <summary>
		/// Returns a new Vector2 with the X component modified.
		/// <br>Example: <code>transform.position = transform.position.WithX(5f);</code></br>
		/// </summary>
		public static Vector2 WithX(this Vector2 v, float x)
		{
			return new Vector2(x, v.y);
		}

		/// <summary>
		/// Returns a new Vector2 with the X component modified.
		/// <br>Example: <code>transform.position = transform.position.WithY(5f);</code></br>
		/// </summary>
		public static Vector2 WithY(this Vector2 v, float y)
		{
			return new Vector2(v.x, y);
		}
	}
}