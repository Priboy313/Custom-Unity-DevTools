using System;
using System.Collections.Generic;

namespace DevTools.Extensions.Experimental
{
	public static class ArrayExtensions
	{
		/// <summary>
		/// Creates a new array with the added item.
		/// <br>If the array is null, returns a new array with one element.</br>
		/// <br>IMPORTANT: Returns a NEW array, does not modify the original!</br>
		/// </summary>
		public static T[] Add<T>(this T[] array, T item)
		{
			if (array == null)
			{
				return new T[] { item };
			}

			T[] newArray = new T[array.Length + 1];
			Array.Copy(array, newArray, array.Length);
			newArray[array.Length] = item;

			return newArray;
		}

		/// <summary>
		/// Creates a new array with added items.
		/// <br>Returns a NEW array with combined elements.</br>
		/// </summary>
		public static T[] AddRange<T>(this T[] array, params T[] items)
		{
			if (array == null)
			{
				return items ?? new T[0];
			}

			if (items == null || items.Length == 0)
			{
				return array;
			}

			T[] newArray = new T[array.Length + items.Length];
			Array.Copy(array, newArray, array.Length);
			Array.Copy(items, 0, newArray, array.Length, items.Length);

			return newArray;
		}


		/// <summary>
		/// Inserts an element into the array at the specified index.
		/// <br>Returns a NEW array with the inserted element.</br>
		/// <br>If the index equals the array length, appends to the end.</br>
		/// </summary>
		public static T[] InsertAt<T>(this T[] array, int index, T item)
		{
			if (array == null)
			{
				if (index == 0)
				{
					return new T[] { item };
				}
				else
				{
					throw new ArgumentOutOfRangeException(
						nameof(index),
						"Cannot insert into null array at non-zero index"
					);
				}
			}

			if (index < 0 || index > array.Length)
			{
				throw new ArgumentOutOfRangeException(
					nameof(index),
					$"Index must be between 0 and {array.Length}"
				);
			}

			T[] newArray = new T[array.Length + 1];

			if (index > 0)
			{
				Array.Copy(array, 0, newArray, 0, index);
			}

			newArray[index] = item;

			if (index < array.Length)
			{
				Array.Copy(array, index, newArray, index + 1, array.Length - index);
			}

			return newArray;
		}

		/// <summary>
		/// Removes an element from the array at the specified index.
		/// <br>Returns a NEW array without the removed element.</br>
		/// </summary>
		public static T[] RemoveAt<T>(this T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException(nameof(array));
			}

			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException(
					nameof(index),
					$"index must be between 0 and {array.Length}"
				);
			}

			if (array.Length == 1)
			{
				return new T[0];
			}

			T[] newArray = new T[array.Length - 1];

			if (index > 0)
			{
				Array.Copy(array, 0, newArray, 0, index);
			}

			if (index < array.Length - 1)
			{
				Array.Copy(array, index + 1, newArray, index, array.Length - index - 1);
			}

			return newArray;
		}

		/// <summary>
		/// Removes the first occurrence of the specified element from the array.
		/// <br>Returns a NEW array without the found element.</br>
		/// <br>If the element is not found, returns a copy of the original array.</br>
		/// </summary>
		public static T[] Remove<T>(this T[] array, T item)
		{
			if (array == null)
			{
				throw new ArgumentNullException(nameof(array));
			}

			int index = Array.IndexOf(array, item);

			if (index >= 0)
			{
				return RemoveAt(array, index);
			}

			T[] copy = new T[array.Length];
			Array.Copy(array, copy, array.Length);

			return copy;
		}

		/// <summary>
		/// Removes all elements from the array that match the specified condition.
		/// <br>Returns a NEW array without the matching elements.</br>
		/// </summary>
		public static T[] RemoveAll<T>(this T[] array, Predicate<T> match)
		{
			if (array == null)
			{
				throw new ArgumentNullException(nameof(array));
			}

			if (match == null)
			{
				throw new ArgumentNullException(nameof(match));
			}

			int countToRemove = 0;

			for (int i = 0; i < array.Length; i++)
			{
				if (match(array[i]))
				{
					countToRemove++;
				}
			}

			if (countToRemove == 0)
			{
				T[] copy = new T[array.Length];
				Array.Copy(array, copy, array.Length);

				return copy;
			}

			T[] newArray = new T[array.Length - countToRemove];
			int newIndex = 0;

			for (int i = 0; i < array.Length; i++)
			{
				if (match(array[i]) == false)
				{
					newArray[newIndex] = array[i];
					newIndex++;
				}
			}

			return newArray;
		}

		/// <summary>
		/// Returns a subarray starting at the specified index with the given length.
		/// <br>The subarray includes elements from startIndex to (startIndex + length - 1).</br>
		/// </summary>
		public static T[] SubArray<T>(this T[] array, int startIndex, int length)
		{
			if (array == null)
			{
				throw new ArgumentNullException(nameof(array));
			}

			if (startIndex < 0 || length < 0 || startIndex + length > array.Length)
			{
				throw new ArgumentOutOfRangeException();
			}

			T[] result = new T[length];
			Array.Copy(array, startIndex, result, 0, length);

			return result;
		}

		/// <summary>
		/// Creates a deep copy of the array.
		/// <br>Returns a NEW array with the same elements.</br>
		/// <br>Returns null if the original array is null.</br>
		/// </summary>
		public static T[] Copy<T>(this T[] array)
		{
			if (array == null)
			{
				return null;
			}

			T[] copy = new T[array.Length];
			Array.Copy(array, copy, array.Length);
			
			return copy;
		}

		/// <summary>
		/// Concatenates two arrays.
		/// <br>Returns a NEW array containing elements from both arrays.</br>
		/// <br>If either array is null, returns a copy of the other array.</br>
		/// </summary>
		public static T[] Concat<T>(this T[] first, T[] second)
		{
			if (first == null)
			{
				return second?.Copy();
			}

			if (second == null)
			{
				return first.Copy();
			}

			T[] result = new T[first.Length + second.Length];
			Array.Copy(first, 0, result, 0, first.Length);
			Array.Copy(second, 0, result, first.Length, second.Length);
			
			return result;
		}
	}
}
