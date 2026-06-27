using NUnit.Framework;
using UnityEngine;
using DevTools.Extensions;

namespace DevTools.Extensions.Tests
{
	public class VectorExtensionsTests
	{
		// --- VECTOR3 TESTS ---

		[TestCase(5f, 10f, 15f, 99f)]
		[TestCase(0f, 0f, 0f, -5.5f)]
		public void WithX_Vector3_ModifiesOnlyX(float x, float y, float z, float newX)
		{
			Vector3 original = new Vector3(x, y, z);
			Vector3 result = original.WithX(newX);

			Assert.AreEqual(newX, result.x, 0.001f);
			Assert.AreEqual(y, result.y, 0.001f);
			Assert.AreEqual(z, result.z, 0.001f);
		}

		[TestCase(5f, 10f, 15f, 99f)]
		[TestCase(0f, 0f, 0f, -5.5f)]
		public void WithY_Vector3_ModifiesOnlyY(float x, float y, float z, float newY)
		{
			Vector3 original = new Vector3(x, y, z);
			Vector3 result = original.WithY(newY);

			Assert.AreEqual(x, result.x, 0.001f);
			Assert.AreEqual(newY, result.y, 0.001f);
			Assert.AreEqual(z, result.z, 0.001f);
		}

		[TestCase(5f, 10f, 15f, 99f)]
		[TestCase(0f, 0f, 0f, -5.5f)]
		public void WithZ_Vector3_ModifiesOnlyZ(float x, float y, float z, float newZ)
		{
			Vector3 original = new Vector3(x, y, z);
			Vector3 result = original.WithZ(newZ);

			Assert.AreEqual(x, result.x, 0.001f);
			Assert.AreEqual(y, result.y, 0.001f);
			Assert.AreEqual(newZ, result.z, 0.001f);
		}

		// --- VECTOR2 TESTS ---

		[TestCase(5f, 10f, 99f)]
		[TestCase(0f, 0f, -5.5f)]
		public void WithX_Vector2_ModifiesOnlyX(float x, float y, float newX)
		{
			Vector2 original = new Vector2(x, y);
			Vector2 result = original.WithX(newX);

			Assert.AreEqual(newX, result.x, 0.001f);
			Assert.AreEqual(y, result.y, 0.001f);
		}

		[TestCase(5f, 10f, 99f)]
		[TestCase(0f, 0f, -5.5f)]
		public void WithY_Vector2_ModifiesOnlyY(float x, float y, float newY)
		{
			Vector2 original = new Vector2(x, y);
			Vector2 result = original.WithY(newY);

			Assert.AreEqual(x, result.x, 0.001f);
			Assert.AreEqual(newY, result.y, 0.001f);
		}
	}
}