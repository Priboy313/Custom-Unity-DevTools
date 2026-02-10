using System;
using System.Collections.Generic;

namespace DevTools.Extensions
{
	public static class CollectionsExtensions
	{
		/// <summary>
		/// Returns a random item from the list.
		/// <br>Returns default(T) if list is null or empty.</br>
		/// </summary>
		public static T GetRandom<T>(this IList<T> list)
		{
			if (list == null || list.Count == 0)
			{
				return default;
			}

			return list[UnityEngine.Random.Range(0, list.Count)];
		}

		/// <summary>
		/// Checks if the collection is null or has no elements.
		/// </summary>
		public static bool IsNullOfEmpty<T>(this ICollection<T> collection)
		{
			return collection == null || collection.Count == 0;
		}

		/// <summary>
		/// Safely checks if the index is within the bounds of the collection.
		/// <br>Returns false if collection is null.</br>
		/// </summary>
		public static bool ContainsIndex<T>(this ICollection<T> collection, int index)
		{
			return collection != null && index >= 0 && index < collection.Count;
		}
	}

}