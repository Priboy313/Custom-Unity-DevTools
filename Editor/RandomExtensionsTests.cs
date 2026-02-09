using NUnit.Framework;
using UnityEngine;
using DevTools.Extensions;

public class RandomExtensionsTests
{
	// --- INT TESTS ---

	[Test]
	public void RandomTo_Int_ReturnsValueWithinRange()
	{
		// Unity Random.Range для int: Max исключен
		int min = 10;
		int max = 20;

		for (int i = 0; i < 100; i++) // Прогоняем много раз для надежности
		{
			int result = min.RandomTo(max);
			Assert.GreaterOrEqual(result, min);
			Assert.Less(result, max); // Строго меньше
		}
	}

	[Test]
	public void RandomTo_Int_SwapsMinMax_IfInverted()
	{
		int result = 20.RandomTo(10); // Вызываем "наоборот"

		Assert.GreaterOrEqual(result, 10);
		Assert.Less(result, 20);
	}

	[Test]
	public void RandomTo_Int_SameValues_ReturnsValue()
	{
		int result = 5.RandomTo(5);
		Assert.AreEqual(5, result);
	}

	// --- FLOAT TESTS ---

	[Test]
	public void RandomTo_Float_ReturnsValueWithinRange()
	{
		// Unity Random.Range для float: Max включен
		float min = 10.5f;
		float max = 20.5f;

		for (int i = 0; i < 100; i++)
		{
			float result = min.RandomTo(max);
			Assert.GreaterOrEqual(result, min);
			Assert.LessOrEqual(result, max); // Меньше или равно
		}
	}

	[Test]
	public void RandomTo_Float_SwapsMinMax()
	{
		float result = 10f.RandomTo(5f);
		Assert.GreaterOrEqual(result, 5f);
		Assert.LessOrEqual(result, 10f);
	}

	// --- VECTOR3 TESTS ---

	[Test]
	public void RandomTo_Vector3_ReturnsComponentsWithinRange()
	{
		Vector3 min = new Vector3(0, 10, 20);
		Vector3 max = new Vector3(5, 15, 25);

		for (int i = 0; i < 50; i++)
		{
			Vector3 result = min.RandomTo(max);

			Assert.GreaterOrEqual(result.x, 0);
			Assert.LessOrEqual(result.x, 5);

			Assert.GreaterOrEqual(result.y, 10);
			Assert.LessOrEqual(result.y, 15);

			Assert.GreaterOrEqual(result.z, 20);
			Assert.LessOrEqual(result.z, 25);
		}
	}

	[Test]
	public void GetRandomPoint_ReturnsPointInsideBounds()
	{
		Bounds bounds = new Bounds(Vector3.zero, new Vector3(10, 10, 10));

		for (int i = 0; i < 50; i++)
		{
			Vector3 point = bounds.GetRandomPoint();
			Assert.IsTrue(bounds.Contains(point), $"Point {point} should be inside bounds");
		}
	}

	// --- PROBABILITY TESTS ---

	[Test]
	public void TryChance_ZeroPercent_AlwaysReturnsFalse()
	{
		// Даже при 1000 попытках 0% не должен сработать
		for (int i = 0; i < 1000; i++)
		{
			Assert.IsFalse(0.TryChance());
			Assert.IsFalse(0f.TryChance());
		}
	}

	[Test]
	public void TryChance_OverHundredPercent_AlwaysReturnsTrue()
	{
		for (int i = 0; i < 1000; i++)
		{
			Assert.IsTrue(101.TryChance());
			Assert.IsTrue(100.1f.TryChance());
		}
	}

	// --- CONVERSION TESTS ---

	[Test]
	public void ToVector3XZ_ConvertsCorrectly()
	{
		Vector2 input = new Vector2(5, 10);
		Vector3 expected = new Vector3(5, 0, 10);

		Vector3 result = input.ToVector3XZ();

		Assert.AreEqual(expected, result);
	}

	[Test]
	public void RandomXZ_ReturnsVectorWithZeroY()
	{
		Vector3 result = Vector3.zero.RandomXZ();
		Assert.AreEqual(0, result.y);
	}

	[Test]
	public void RandomXZ_ReturnsNormalizedVector()
	{
		// В коде используется .normalized, значит длина должна быть 1
		Vector3 result = Vector3.zero.RandomXZ();

		// Используем дельту, так как float может быть неточным
		Assert.AreEqual(1f, result.magnitude, 0.001f);
	}
}