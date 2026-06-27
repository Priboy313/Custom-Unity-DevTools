using NUnit.Framework;
using UnityEngine;
using DevTools.Extensions;

namespace DevTools.Extensions.Tests
{
	public class UIExtensionsTests
	{
		private GameObject _go;
		private CanvasGroup _canvasGroup;

		[SetUp]
		public void Setup()
		{
			_go = new GameObject("TestUI");
			_canvasGroup = _go.AddComponent<CanvasGroup>();
		}

		[TearDown]
		public void Teardown()
		{
			if (_go != null)
			{
				Object.DestroyImmediate(_go);
			}
		}

		[TestCase(true, 1f, true, true)]
		[TestCase(false, 0f, false, false)]
		public void SetVisibility_SetsCorrectProperties(bool isVisible, float expectedAlpha, bool expectedInteractable, bool expectedBlocksRaycasts)
		{
			_canvasGroup.SetVisibility(isVisible);

			Assert.AreEqual(expectedAlpha, _canvasGroup.alpha, 0.001f);
			Assert.AreEqual(expectedInteractable, _canvasGroup.interactable);
			Assert.AreEqual(expectedBlocksRaycasts, _canvasGroup.blocksRaycasts);
		}

		[Test]
		public void SetVisibility_NullCanvasGroup_DoesNotThrow()
		{
			CanvasGroup nullGroup = null;
			Assert.DoesNotThrow(() => nullGroup.SetVisibility(true));
		}
	}
}