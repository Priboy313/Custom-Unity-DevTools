using NUnit.Framework;
using System.Collections.Generic;
using DevTools.Extensions;

public class CollectionsExtensionsTests
{
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
}