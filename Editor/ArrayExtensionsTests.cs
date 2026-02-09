using NUnit.Framework;
using System;
using DevTools.Extensions.Experimental;

public class ArrayExtensionsTests
{
	// --- ADD TESTS ---

	[Test]
	public void Add_ValidArray_AddsItemToEnd()
	{
		int[] source = { 1, 2 };
		int[] result = source.Add(3);

		CollectionAssert.AreEqual(new int[] { 1, 2, 3 }, result);
		Assert.AreNotSame(source, result); // Проверка, что создан новый массив
	}

	[Test]
	public void Add_NullArray_ReturnsNewArrayWithItem()
	{
		int[] source = null;
		int[] result = source.Add(5);

		CollectionAssert.AreEqual(new int[] { 5 }, result);
	}

	// --- ADD RANGE TESTS ---

	[Test]
	public void AddRange_ValidArrays_CombinesThem()
	{
		int[] source = { 1, 2 };
		int[] result = source.AddRange(3, 4);

		CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4 }, result);
	}

	[Test]
	public void AddRange_NullSource_ReturnsItems()
	{
		int[] source = null;
		int[] result = source.AddRange(1, 2);

		CollectionAssert.AreEqual(new int[] { 1, 2 }, result);
	}

	[Test]
	public void AddRange_EmptyItems_ReturnsOriginalCopy()
	{
		int[] source = { 1, 2 };
		int[] result = source.AddRange(); // Ничего не добавляем

		CollectionAssert.AreEqual(source, result);
	}

	// --- INSERT AT TESTS ---

	[Test]
	public void InsertAt_Middle_InsertsCorrectly()
	{
		int[] source = { 1, 3 };
		int[] result = source.InsertAt(1, 2); // Вставляем 2 по индексу 1

		CollectionAssert.AreEqual(new int[] { 1, 2, 3 }, result);
	}

	[Test]
	public void InsertAt_Start_InsertsAtZero()
	{
		int[] source = { 2, 3 };
		int[] result = source.InsertAt(0, 1);

		CollectionAssert.AreEqual(new int[] { 1, 2, 3 }, result);
	}

	[Test]
	public void InsertAt_End_Appends()
	{
		int[] source = { 1, 2 };
		int[] result = source.InsertAt(2, 3); // Индекс равен длине

		CollectionAssert.AreEqual(new int[] { 1, 2, 3 }, result);
	}

	[Test]
	public void InsertAt_NullArray_IndexZero_CreatesArray()
	{
		int[] source = null;
		int[] result = source.InsertAt(0, 5);

		CollectionAssert.AreEqual(new int[] { 5 }, result);
	}

	[Test]
	public void InsertAt_OutOfBounds_ThrowsException()
	{
		int[] source = { 1 };
		Assert.Throws<ArgumentOutOfRangeException>(() => source.InsertAt(5, 2));
		Assert.Throws<ArgumentOutOfRangeException>(() => source.InsertAt(-1, 2));
	}

	// --- REMOVE AT TESTS ---

	[Test]
	public void RemoveAt_Middle_RemovesCorrectly()
	{
		int[] source = { 1, 2, 3 };
		int[] result = source.RemoveAt(1); // Удаляем 2

		CollectionAssert.AreEqual(new int[] { 1, 3 }, result);
	}

	[Test]
	public void RemoveAt_LastElement_ReturnsEmptyArray()
	{
		int[] source = { 5 };
		int[] result = source.RemoveAt(0);

		Assert.AreEqual(0, result.Length);
	}

	[Test]
	public void RemoveAt_NullArray_ThrowsException()
	{
		int[] source = null;
		Assert.Throws<ArgumentNullException>(() => source.RemoveAt(0));
	}

	[Test]
	public void RemoveAt_OutOfBounds_ThrowsException()
	{
		int[] source = { 1, 2 };
		Assert.Throws<ArgumentOutOfRangeException>(() => source.RemoveAt(5));
	}

	// --- REMOVE ITEM TESTS ---

	[Test]
	public void Remove_ItemExists_RemovesFirstOccurrence()
	{
		int[] source = { 1, 2, 2, 3 };
		int[] result = source.Remove(2);

		CollectionAssert.AreEqual(new int[] { 1, 2, 3 }, result);
	}

	[Test]
	public void Remove_ItemNotExists_ReturnsCopy()
	{
		int[] source = { 1, 2, 3 };
		int[] result = source.Remove(99);

		CollectionAssert.AreEqual(source, result);
		Assert.AreNotSame(source, result); // Убеждаемся, что вернулась копия
	}

	// --- REMOVE ALL TESTS ---

	[Test]
	public void RemoveAll_MatchesExist_RemovesAllMatches()
	{
		int[] source = { 1, 2, 3, 4, 5, 6 };
		// Удаляем все четные
		int[] result = source.RemoveAll(x => x % 2 == 0);

		CollectionAssert.AreEqual(new int[] { 1, 3, 5 }, result);
	}

	[Test]
	public void RemoveAll_NoMatches_ReturnsCopy()
	{
		int[] source = { 1, 3, 5 };
		int[] result = source.RemoveAll(x => x % 2 == 0); // Четных нет

		CollectionAssert.AreEqual(source, result);
	}

	[Test]
	public void RemoveAll_AllMatch_ReturnsEmpty()
	{
		int[] source = { 2, 4, 6 };
		int[] result = source.RemoveAll(x => x % 2 == 0);

		Assert.AreEqual(0, result.Length);
	}

	// --- SUBARRAY TESTS ---

	[Test]
	public void SubArray_ValidRange_ReturnsSlice()
	{
		int[] source = { 10, 20, 30, 40, 50 };
		// Берем с индекса 1, длина 3 (20, 30, 40)
		int[] result = source.SubArray(1, 3);

		CollectionAssert.AreEqual(new int[] { 20, 30, 40 }, result);
	}

	[Test]
	public void SubArray_InvalidRange_ThrowsException()
	{
		int[] source = { 1, 2, 3 };
		// Пытаемся взять больше, чем есть
		Assert.Throws<ArgumentOutOfRangeException>(() => source.SubArray(1, 10));
	}

	// --- COPY TESTS ---

	[Test]
	public void Copy_ValidArray_ReturnsDeepCopy()
	{
		int[] source = { 1, 2, 3 };
		int[] result = source.Copy();

		CollectionAssert.AreEqual(source, result);
		Assert.AreNotSame(source, result);
	}

	[Test]
	public void Copy_NullArray_ReturnsNull()
	{
		int[] source = null;
		int[] result = source.Copy();

		Assert.IsNull(result);
	}

	// --- CONCAT TESTS ---

	[Test]
	public void Concat_TwoArrays_JoinsThem()
	{
		int[] first = { 1, 2 };
		int[] second = { 3, 4 };
		int[] result = first.Concat(second);

		CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4 }, result);
	}

	[Test]
	public void Concat_FirstNull_ReturnsSecondCopy()
	{
		int[] first = null;
		int[] second = { 3, 4 };
		int[] result = first.Concat(second);

		CollectionAssert.AreEqual(second, result);
	}

	[Test]
	public void Concat_SecondNull_ReturnsFirstCopy()
	{
		int[] first = { 1, 2 };
		int[] second = null;
		int[] result = first.Concat(second);

		CollectionAssert.AreEqual(first, result);
	}
}