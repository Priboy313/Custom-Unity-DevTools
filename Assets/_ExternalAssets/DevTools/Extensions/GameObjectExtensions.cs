using UnityEngine;

namespace DevTools.Extensions
{
	public static class GameObjectExtensions
	{
		/// <summary>
		/// Gets the component of type T if it exists, or adds it if it doesn't.
		/// <br>Uses TryGetComponent to prevent garbage allocations.</br>
		/// </summary>
		public static T GetOrAddComponent<T>(this GameObject go) where T : Component
		{
			return go.TryGetComponent<T>(out var component) ? component : go.AddComponent<T>();
		}

		/// <summary>
		/// Recursively sets the layer of this GameObject and all of its children.
		/// </summary>
		public static void SetLayerRecursively(this GameObject go, int layer)
		{
			go.layer = layer;
			foreach (Transform child in go.transform)
			{
				child.gameObject.SetLayerRecursively(layer);
			}
		}
	}
}