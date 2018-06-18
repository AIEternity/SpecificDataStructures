# SpecificDataStructures
Specific data structures 

## Installing
```
 PM> Install-Package SpecificDataStructures 
```
## FixedList
### Description
Fixed-size collection that, once the size limit has been reached, replaces the oldest record whenever a new one is added
### Example
```csharp
FixedList<int> lst = new FixedList<int>(3);
lst.Add(0);
lst.Add(1);
lst.Add(2);
lst.Add(3);
lst.Add(4);
Assert.Equal(2, lst[0]);
Assert.Equal(3, lst[1]);
Assert.Equal(4, lst[2]);
```
## VersionArray
### Description
VersionArray is memory-efficient structure for almost identical arrays. Each instance only keeps  difference between parent and child array.
### Example
```csharp
VersionArray<int> arr = new VersionArray<int>(0, 1, 2);
var arr2 = arr.NewBranch();
arr2[1] = 5;
var arr3 = arr2.NewBranch();
arr3[2] = 10;
var arr4 = arr.NewBranch();
arr4[1] = 11;
arr4[0] = 12;
Assert.True(Enumerable.SequenceEqual(new[] { 0, 1, 2 }, arr));
Assert.True(Enumerable.SequenceEqual(new[] { 0, 5, 2 }, arr2));
Assert.True(Enumerable.SequenceEqual(new[] { 0, 5, 10 }, arr3));
Assert.True(Enumerable.SequenceEqual(new[] { 12, 11, 2 }, arr4));
```

