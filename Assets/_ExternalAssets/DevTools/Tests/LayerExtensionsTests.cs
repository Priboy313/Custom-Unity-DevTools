using NUnit.Framework;
using UnityEngine;

namespace DevTools.Extensions.Tests
{
	public class LayerExtensionsTests
	{
		// --- CONTAINS (INT LAYER INDEX) TESTS ---

		[TestCase(0, 0, true)]
		[TestCase(5, 5, true)]
		[TestCase(31, 31, true)]
		[TestCase(10, 5, false)]
		public void Contains_Index_SingleLayerMatches(int maskLayer, int checkLayer, bool expected)
		{
			LayerMask mask = 1 << maskLayer;
			Assert.AreEqual(expected, mask.Contains(checkLayer));
		}

		[TestCase(1, true)]
		[TestCase(15, true)]
		[TestCase(30, true)]
		[TestCase(3, false)]
		public void Contains_Index_MultiLayerMask(int checkLayer, bool expected)
		{
			LayerMask mask = (1 << 1) | (1 << 15) | (1 << 30);
			Assert.AreEqual(expected, mask.Contains(checkLayer));
		}

		[Test]
		public void Contains_Index_EmptyMask_ReturnsFalse()
		{
			LayerMask mask = 0;
			Assert.IsFalse(mask.Contains(5));
		}

		[TestCase(0)]
		[TestCase(15)]
		[TestCase(31)]
		public void Contains_Index_EverythingMask_ReturnsTrue(int checkLayer)
		{
			LayerMask mask = -1;
			Assert.IsTrue(mask.Contains(checkLayer));
		}

		// --- CONTAINS (GAMEOBJECT) TESTS ---

		[TestCase(12, 12, true)] 
		[TestCase(10, 5, false)] 
		public void Contains_GameObject_ChecksLayer(int maskLayer, int goLayer, bool expected)
		{
			GameObject go = new GameObject("TestObject");
			go.layer = goLayer;

			LayerMask mask = 1 << maskLayer;
			bool result = mask.Contains(go);

			Object.DestroyImmediate(go);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void Contains_GameObject_Null_ReturnsFalse()
		{
			LayerMask mask = -1;
			Assert.IsFalse(mask.Contains((GameObject)null));
		}
	}
}