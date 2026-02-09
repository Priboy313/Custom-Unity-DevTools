# 🛠 DevTools Experimental Extensions

A collection of high-utility C# Extension Methods for Unity.
This package aims to reduce boilerplate code, improve readability, and make common operations (like random generation or layer checking) more intuitive.

## 📦 Installation

1. Copy the `DevTools` folder into your project's `Assets` directory.
2. Ensure you include the namespace in your scripts:

```C#
using DevTools.Extensions.Experimental;
```

## 🔄 Array Extensions
Important: All array extension methods return NEW arrays. Original arrays remain unchanged.

## Adding Elements
### Add
Adds an element to the end of the array. If the array is null, returns a new array with one element.

```C#
int[] array = new int[] { 1, 2, 3 };
array = array.Add(4); // [1, 2, 3, 4]

// Adding to null array
int[] empty = null;
empty = empty.Add(1); // [1]
```

### AddRange
Adds multiple elements to the end of the array.

```C#
int[] array = new int[] { 1, 2, 3 };
array = array.AddRange(4, 5, 6); // [1, 2, 3, 4, 5, 6]

// With another array
int[] moreNumbers = { 7, 8 };
array = array.AddRange(moreNumbers); // [1, 2, 3, 4, 5, 6, 7, 8]
```

### InsertAt
Inserts an element at the specified index. If index equals array length, adds to the end.

```C#
int[] array = new int[] { 1, 2, 4 };
array = array.InsertAt(2, 3); // [1, 2, 3, 4]

// Insert at beginning
array = array.InsertAt(0, 0); // [0, 1, 2, 3, 4]
```
## Removing Elements
### RemoveAt
Removes an element at the specified index.

```C#
int[] array = new int[] { 1, 2, 3, 4 };
array = array.RemoveAt(1); // [1, 3, 4]

// Throws exception if index is out of range
```

### Remove
Removes the first occurrence of the specified element.

```C#
int[] array = new int[] { 1, 2, 3, 2 };
array = array.Remove(2); // [1, 3, 2]

// Returns copy if element not found
array = array.Remove(5); // [1, 3, 2] (unchanged)
```
### RemoveAll
Removes all elements that match the predicate condition.
```C#
int[] array = new int[] { 1, 2, 3, 4, 5 };
array = array.RemoveAll(x => x % 2 == 0); // [1, 3, 5]

// Remove all strings starting with 'A'
string[] names = { "Alice", "Bob", "Anna", "Charlie" };
names = names.RemoveAll(name => name.StartsWith("A")); // ["Bob", "Charlie"]
```

## Array Manipulation
### SubArray
Returns a portion of the array starting at the specified index with given length.

```C#
int[] array = new int[] { 1, 2, 3, 4, 5 };
int[] sub = array.SubArray(1, 3); // [2, 3, 4]

// Throws exception if parameters are invalid
```

## Copy
Creates a deep copy of the array.

```C#
int[] original = new int[] { 1, 2, 3 };
int[] copy = original.Copy(); // [1, 2, 3]

// Copies reference types too
GameObject[] gameObjects = new GameObject[3];
GameObject[] copy = gameObjects.Copy(); // New array with same references
```
### Concat
Concatenates two arrays.

```C#
int[] first = new int[] { 1, 2 };
int[] second = new int[] { 3, 4 };
int[] result = first.Concat(second); // [1, 2, 3, 4]

// Handles null arrays gracefully
int[] result2 = first.Concat(null); // [1, 2] (copy of first)
```
## Performance Notes
- All operations create new arrays - consider using List<T> for frequent modifications

- Time complexity: Most operations are O(n) where n is array length

- Memory: Each operation creates a new array, doubling memory usage temporarily
