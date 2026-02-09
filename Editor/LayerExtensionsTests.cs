using NUnit.Framework;
using UnityEngine;
using DevTools.Extensions;

public class LayerExtensionsTests
{
	// --- CONTAINS (INT LAYER INDEX) TESTS ---

	[Test]
	public void Contains_Index_SingleLayer_ReturnsTrueIfMatch()
	{
		int targetLayer = 5;
		// Создаем маску вручную, сдвигая бит
		LayerMask mask = 1 << targetLayer;

		Assert.IsTrue(mask.Contains(targetLayer));
	}

	[Test]
	public void Contains_Index_SingleLayer_ReturnsFalseIfNoMatch()
	{
		int targetLayer = 5;
		int maskLayer = 10;
		LayerMask mask = 1 << maskLayer;

		Assert.IsFalse(mask.Contains(targetLayer));
	}

	[Test]
	public void Contains_Index_MultiLayerMask_ReturnsTrueForIncludedLayers()
	{
		int layerA = 1;
		int layerB = 15;
		int layerC = 30;

		// Создаем маску, включающую 3 разных слоя (побитовое ИЛИ)
		LayerMask mask = (1 << layerA) | (1 << layerB) | (1 << layerC);

		Assert.IsTrue(mask.Contains(layerA), "Should contain Layer A");
		Assert.IsTrue(mask.Contains(layerB), "Should contain Layer B");
		Assert.IsTrue(mask.Contains(layerC), "Should contain Layer C");
	}

	[Test]
	public void Contains_Index_MultiLayerMask_ReturnsFalseForExcludedLayer()
	{
		int layerA = 1;
		int layerB = 5;

		LayerMask mask = (1 << layerA) | (1 << layerB);
		int excludedLayer = 3;

		Assert.IsFalse(mask.Contains(excludedLayer));
	}

	[Test]
	public void Contains_Index_EmptyMask_ReturnsFalse()
	{
		LayerMask mask = 0; // Ничего не выбрано
		Assert.IsFalse(mask.Contains(5));
	}

	[Test]
	public void Contains_Index_EverythingMask_ReturnsTrue()
	{
		// -1 в int соответствует всем выставленным битам (1111...)
		LayerMask mask = -1;
		Assert.IsTrue(mask.Contains(0));
		Assert.IsTrue(mask.Contains(15));
		Assert.IsTrue(mask.Contains(31));
	}

	// --- CONTAINS (GAMEOBJECT) TESTS ---

	[Test]
	public void Contains_GameObject_ReturnsTrueIfLayerMatches()
	{
		// Создаем временный объект
		GameObject go = new GameObject("TestObject");
		int targetLayer = 12;
		go.layer = targetLayer;

		LayerMask mask = 1 << targetLayer;

		bool result = mask.Contains(go);

		// Обязательно удаляем объект, так как тесты могут бежать в редакторе
		Object.DestroyImmediate(go);

		Assert.IsTrue(result);
	}

	[Test]
	public void Contains_GameObject_ReturnsFalseIfLayerMismatch()
	{
		GameObject go = new GameObject("TestObject");
		go.layer = 5; // Слой объекта: 5

		LayerMask mask = 1 << 10; // Маска разрешает только слой 10

		bool result = mask.Contains(go);

		Object.DestroyImmediate(go);

		Assert.IsFalse(result);
	}
}