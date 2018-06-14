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
