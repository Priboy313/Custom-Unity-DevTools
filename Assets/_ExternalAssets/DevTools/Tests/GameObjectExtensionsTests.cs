using NUnit.Framework;
using UnityEngine;
using DevTools.Extensions;

namespace DevTools.Extensions.Tests
{
	public class GameObjectExtensionsTests
	{
		private GameObject _rootGo;

		[SetUp]
		public void Setup()
		{
			_rootGo = new GameObject("RootTestObject");
		}

		[TearDown]
		public void Teardown()
		{
			if (_rootGo != null)
			{
				Object.DestroyImmediate(_rootGo);
			}
		}

		[Test]
		public void GetOrAddComponent_ComponentDoesNotExist_AddsAndReturnsIt()
		{
			Assert.IsNull(_rootGo.GetComponent<BoxCollider>());

			BoxCollider result = _rootGo.GetOrAddComponent<BoxCollider>();

			Assert.IsNotNull(result);
			Assert.IsNotNull(_rootGo.GetComponent<BoxCollider>());
		}

		[Test]
		public void GetOrAddComponent_ComponentExists_ReturnsExistingWithoutDuplicates()
		{
			BoxCollider first = _rootGo.AddComponent<BoxCollider>();

			BoxCollider result = _rootGo.GetOrAddComponent<BoxCollider>();

			Assert.AreSame(first, result);
			Assert.AreEqual(1, _rootGo.GetComponents<BoxCollider>().Length);
		}

		[Test]
		public void SetLayerRecursively_PropagatesLayerToAllChildren()
		{
			GameObject child = new GameObject("Child");
			child.transform.SetParent(_rootGo.transform);

			GameObject grandchild = new GameObject("Grandchild");
			grandchild.transform.SetParent(child.transform);

			int targetLayer = 8; // Слой PostProcessing (или любой свободный)

			// Действие
			_rootGo.SetLayerRecursively(targetLayer);

			// Проверка
			Assert.AreEqual(targetLayer, _rootGo.layer);
			Assert.AreEqual(targetLayer, child.layer);
			Assert.AreEqual(targetLayer, grandchild.layer);
		}
	}
}