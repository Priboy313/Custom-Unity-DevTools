# 🛠 DevTools Extensions

A collection of high-utility C# Extension Methods for Unity.
This package aims to reduce boilerplate code, improve readability, and make common operations (like random generation or layer checking) more intuitive.

## 📦 Installation

1. Copy the `DevTools` folder into your project's `Assets` directory.
2. Ensure you include the namespace in your scripts:

```C#
using DevTools.Extensions;
```

# 🎲 Random Extensions

Fluent syntax for UnityEngine.Random. No more manually calculating min/max or writing bulky probability checks.
Range Generation

Smart range finder. Automatically swaps min/max if they are passed in the wrong order.
```C#
// Integers: Get a random damage between 10 and 20
int damage = 10.RandomTo(20);

// Floats: Get a random delay
float delay = 0.5f.RandomTo(2.5f);

// Vectors: Random position between two points
transform.position = _startPoint.position.RandomTo(_endPoint.position);
```

## Probability (Chance)

Readable syntax for percentage-based checks.
``` C#
// 30% chance to drop loot
if (30.TryChance())
{
    DropLoot();
}

// 0.5% chance for a critical hit (supports floats)
if (0.5f.TryChance())
{
    ApplyCrit();
}
```

## Spatial Random & Bounds

Great for spawning systems.
``` C#
[SerializeField] private BoxCollider _spawnArea;

void Spawn()
{
    // Get a random point INSIDE the collider bounds
    Vector3 spawnPos = _spawnArea.bounds.GetRandomPoint();
}
```

## Plane Conversion

Helpers to work with the XZ plane (ground).
``` C#
// Get a random point on the ground (Y = 0) within a unit circle
Vector3 randomPos = Vector3.zero.RandomXZ(); 

// Convert any Vector2 to Vector3 (X, 0, Y)
Vector3 groundPos = Random.insideUnitCircle.ToVector3XZ();
```
# 🧱 Layer Extensions

Forget about bitwise operators (1 << layer) logic in your gameplay code.

```C#
[SerializeField] private LayerMask _groundMask;

private void OnCollisionEnter(Collision other)
{
    // OLD WAY:
    // if ((_groundMask.value & (1 << other.gameObject.layer)) > 0) { ... }

    // NEW WAY:
    if (_groundMask.Contains(other.gameObject))
    {
        Land();
    }
    
    // Also supports layer index directly
    if (_groundMask.Contains(other.gameObject.layer)) { ... }
}
```

## 📦 Collection Extensions

Safe methods to work with Lists, Arrays, and other collections implementing `IList<T>`.

### Get Random Item
Safely retrieves a random element. Returns `default` (null) if the collection is empty or null, preventing errors.

```C#
// 1. Works with Lists
List<GameObject> enemies = new List<GameObject>();
GameObject enemy = enemies.GetRandom();

// 2. Works with Arrays
string[] greetings = { "Hello", "Hi", "Greetings" };
string randomWord = greetings.GetRandom();

// 3. Safe handling of empty collections
int[] emptyArray = new int[0];
int result = emptyArray.GetRandom(); // Returns 0 (default int), no errors
```

### Validation (Null or Empty)

Quickly check if a list requires initialization or has no elements.

```C#
List<string> items = null;

// Replaces: if (items == null || items.Count == 0)
if (items.IsNullOrEmpty())
{
    items = new List<string>();
}
```

### Safe Index Check

Checks if an index is within the valid bounds of the collection (and checks for null). Useful before accessing arrays by index.

```C#
int index = 10;

// Replaces: if (list != null && index >= 0 && index < list.Count)
if (enemies.ContainsIndex(index))
{
    Attack(enemies[index]);
}
```


# 🔄 Transform Extensions

Shortcuts for common transform operations.

## Reset

Resets localPosition, localRotation, and localScale. Acts exactly like the "Reset" button in the Unity Inspector.
``` C#
// Reset to local zero and scale 1
transform.Reset();
```

## ResetWorld

Resets global world coordinates to Vector3.zero.
``` C#
// Reset to world zero
transform.ResetWorld();
```

# [Experimental Extensions](/Experimental/README.md)

Non-optimal extensions, not for regualr projects use.