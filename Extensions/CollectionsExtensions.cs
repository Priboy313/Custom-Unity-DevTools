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
	}
}