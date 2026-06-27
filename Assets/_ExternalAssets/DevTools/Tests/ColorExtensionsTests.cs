using NUnit.Framework;
using UnityEngine;
using DevTools.Extensions;

namespace DevTools.Extensions.Tests
{
	public class ColorExtensionsTests
	{
		[TestCase(0f)]
		[TestCase(0.5f)]
		[TestCase(1f)]
		public void WithAlpha_ModifiesOnlyAlpha(float newAlpha)
		{
			Color original = new Color(0.1f, 0.2f, 0.3f, 1.0f);
			Color result = original.WithAlpha(newAlpha);

			Assert.AreEqual(0.1f, result.r, 0.001f);
			Assert.AreEqual(0.2f, result.g, 0.001f);
			Assert.AreEqual(0.3f, result.b, 0.001f);
			Assert.AreEqual(newAlpha, result.a, 0.001f);
		}
	}
}