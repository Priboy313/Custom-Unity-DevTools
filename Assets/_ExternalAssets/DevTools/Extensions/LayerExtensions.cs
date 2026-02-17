using UnityEngine;

namespace DevTools.Extensions
{
	public static class LayerExtensions
	{
		/// <summary>
		/// Checks if the LayerMask contains a specific layer index.
		/// <br>Example: <code>groundMask.Contains(other.gameObject.layer)</code></br>
		/// </summary>
		/// <param name="mask">The LayerMask to check.</param>
		/// <param name="layerIndex">The layer index (0-31).</param>
		public static bool Contains(this LayerMask mask, int layerIndex)
		{
			return ((1 << layerIndex) & mask) != 0;
		}


		/// <summary>
		/// Checks if the LayerMask contains the layer of a specific GameObject.
		/// <br>Example: <code>groundMask.Contains(collision.gameObject)</code></br>
		/// </summary>
		/// <param name="mask">The LayerMask to check.</param>
		/// <param name="gameObject">The GameObject to check.</param>
		public static bool Contains(this LayerMask mask, GameObject gameObject)
		{
			return (mask.value & (1 << gameObject.layer)) != 0;
		}
	}

}