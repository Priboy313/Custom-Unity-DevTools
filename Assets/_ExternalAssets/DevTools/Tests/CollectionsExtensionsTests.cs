using NUnit.Framework;
using System.Collections.Generic;


namespace DevTools.Extensions.Tests
{
	public class CollectionsExtensionsTests
	{
		// --- GET_RANDOM TESTS ---

		[Test]
		public void GetRandom_NullList_ReturnsDefault()
		{
			// Reference type (string) -> default is null
			IList<string> list = null;
			string result = list.GetRandom();

			Assert.IsNull(result);
		}

		[Test]
		public void GetRandom_EmptyList_ReturnsDefaultValueType()
		{
			// Value type (int) -> default is 0
			List<int> list = new List<int>();
			int result = list.GetRandom();

			Assert.AreEqual(0, result);
		}

		[Test]
		public void GetRandom_SingleItem_ReturnsThatItem()
		{
			var list = new List<string> { "Solo" };

			string result = list.GetRandom();

			Assert.AreEqual("Solo", result);
		}

		[Test]
		public void GetRandom_MultipleItems_ReturnsItemContainedInList()
		{
			var list = new List<int> { 10, 20, 30, 40, 50 };

			// Прогоняем несколько раз для надежности, так как рандом
			for (int i = 0; i < 20; i++)
			{
				int result = list.GetRandom();

				// Проверяем, что полученный элемент реально есть в исходном списке
				CollectionAssert.Contains(list, result, $"Result {result} was not found in the source list");
			}
		}

		[Test]
		public void GetRandom_WorksWithArrays()
		{
			// Проверка, что расширение IList<T> подхватывает и обычные массивы
			string[] array = { "Apple", "Banana", "Cherry" };

			string result = array.GetRandom();

			CollectionAssert.Contains(array, result);
		}

		// --- IS_NULL_OR_EMPTY TESTS ---

		[Test]
		public void IsNullOrEmpty_NullCollection_ReturnsTrue()
		{
			List<int> list = null;
			Assert.IsTrue(list.IsNullOrEmpty());
		}

		[Test]
		public void IsNullOrEmpty_EmptyCollection_ReturnsTrue()
		{
			var list = new List<string>();
			Assert.IsTrue(list.IsNullOrEmpty());
		}

		[Test]
		public void IsNullOrEmpty_WithElements_ReturnsFalse()
		{
			var list = new List<int> { 1, 2, 3 };
			Assert.IsFalse(list.IsNullOrEmpty());
		}

		// --- CONTAINS_INDEX TESTS ---

		[TestCase(0, true)]   // Первый элемент (валидный)
		[TestCase(2, true)]   // Последний элемент (валидный)
		[TestCase(-1, false)]  // Отрицательный индекс (невалидный)
		[TestCase(3, false)]   // Индекс за пределами длины (невалидный)
		public void ContainsIndex_ChecksCorrectly(int index, bool expected)
		{
			var list = new List<string> { "A", "B", "C" };
			Assert.AreEqual(expected, list.ContainsIndex(index));
		}

		[Test]
		public void ContainsIndex_NullCollection_ReturnsFalse()
		{
			List<int> list = null;
			Assert.IsFalse(list.ContainsIndex(0));
		}
	}
}
