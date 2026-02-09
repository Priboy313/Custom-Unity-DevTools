using NUnit.Framework;
using UnityEngine;
using DevTools.Extensions;

public class TransformExtensionsTests
{
	private GameObject _testObject;
	private GameObject _parentObject;

	[SetUp]
	public void Setup()
	{
		// Создаем чистые объекты перед каждым тестом
		_testObject = new GameObject("TestObject");
		_parentObject = new GameObject("ParentObject");
	}

	[TearDown]
	public void Teardown()
	{
		// Обязательно удаляем мусор со сцены
		Object.DestroyImmediate(_testObject);
		Object.DestroyImmediate(_parentObject);
	}

	// --- RESET (LOCAL) TESTS ---

	[Test]
	public void Reset_ResetsLocalValuesToDefault()
	{
		Transform t = _testObject.transform;

		// Задаем "грязные" значения
		t.localPosition = new Vector3(10, 20, 30);
		t.localRotation = Quaternion.Euler(45, 90, 180);
		t.localScale = new Vector3(2, 3, 4);

		// Действие
		t.Reset();

		// Проверка
		Assert.AreEqual(Vector3.zero, t.localPosition);
		Assert.AreEqual(Quaternion.identity, t.localRotation);
		Assert.AreEqual(Vector3.one, t.localScale);
	}

	[Test]
	public void Reset_WithParent_ResetsRelativeToParent()
	{
		Transform child = _testObject.transform;
		Transform parent = _parentObject.transform;

		// Настраиваем родителя
		parent.position = new Vector3(100, 0, 0);
		child.SetParent(parent);

		// Сдвигаем ребенка локально
		child.localPosition = new Vector3(5, 0, 0);
		// Сейчас его World Position = 105

		// Действие
		child.Reset();

		// Проверка: Локально он должен стать 0, а в Мире - встать в позицию родителя
		Assert.AreEqual(Vector3.zero, child.localPosition);
		Assert.AreEqual(parent.position, child.position); // World pos == Parent pos
	}

	// --- RESET WORLD TESTS ---

	[Test]
	public void ResetWorld_ResetsGlobalValuesToDefault()
	{
		Transform t = _testObject.transform;

		t.position = new Vector3(50, 60, 70);
		t.rotation = Quaternion.Euler(10, 20, 30);
		t.localScale = new Vector3(0.5f, 0.5f, 0.5f);

		// Действие
		t.ResetWorld();

		// Проверка
		Assert.AreEqual(Vector3.zero, t.position);
		Assert.AreEqual(Quaternion.identity, t.rotation);
		// Scale в Unity всегда локальный, но ResetWorld ставит его в 1
		Assert.AreEqual(Vector3.one, t.localScale);
	}

	[Test]
	public void ResetWorld_WithParent_MovesToWorldZero()
	{
		Transform child = _testObject.transform;
		Transform parent = _parentObject.transform;

		// Настраиваем родителя
		parent.position = new Vector3(100, 100, 100);
		child.SetParent(parent);

		// Сдвигаем ребенка куда-то
		child.position = new Vector3(150, 150, 150);

		// Действие
		child.ResetWorld();

		// Проверка:
		// 1. В мире он должен быть в 0
		Assert.AreEqual(Vector3.zero, child.position);

		// 2. Локально он должен быть смещен относительно родителя (-100, -100, -100)
		// Используем дельту для float сравнений векторов
		float delta = 0.001f;
		Assert.AreEqual(-100f, child.localPosition.x, delta);
		Assert.AreEqual(-100f, child.localPosition.y, delta);
		Assert.AreEqual(-100f, child.localPosition.z, delta);
	}
}