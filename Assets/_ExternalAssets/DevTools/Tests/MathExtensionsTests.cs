using NUnit.Framework;
using UnityEngine;
using DevTools.Extensions;

namespace DevTools.Extensions.Tests
{
	public class MathExtensionsTests
	{
		[TestCase(50f, 0f, 100f, 0f, 1f, 0.5f)]       // Стандартный ремап (середина диапазона)
		[TestCase(25f, 0f, 50f, 10f, 20f, 15f)]       // Смещение с масштабированием
		[TestCase(0f, -10f, 10f, -1f, 1f, 0f)]        // Работа с отрицательными диапазонами
		[TestCase(150f, 0f, 100f, 0f, 10f, 15f)]      // Экстраполяция (выход за пределы)
		public void Remap_CalculatesCorrectly(float val, float from1, float to1, float from2, float to2, float expected)
		{
			float result = val.Remap(from1, to1, from2, to2);
			Assert.AreEqual(expected, result, 0.001f);
		}

		[Test]
		public void Remap_EmptySourceRange_ReturnsFromTarget()
		{
			// Предотвращение деления на ноль (когда диапазон равен нулю)
			float val = 5f;
			float result = val.Remap(10f, 10f, 50f, 100f);

			Assert.AreEqual(50f, result);
		}
	}
}