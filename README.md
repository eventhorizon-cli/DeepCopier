# DeepCopier

DeepCopier is a small library can deep copy object by Expression Tree.

## Installation:
Install DeepCopier [NuGet package](https://www.nuget.org/packages/DeepCopier/).

## Usage Examples:

### 1.Deep copy the source object.
```C#
SomeType obj1 = new SomeType();
SomeType obj2 = Copier.Copy(obj1);

List<SomeType> list1 = new List<SomeType>{ obj1 };
List<SomeType> list2 = Copier.Copy(list1);
```

### 2.Create a new instance of the target type, and deep copy the property values of the given source object into the target instance.
```C#
/* The source and target classes do not have to match or even be derived
   from each other, as long as the properties match. */
SomeType obj1 = new SomeType();
AnotherType obj2 = Copier.Copy<SomeType, AnotherType>(obj1);
```

### 3.Copy the property values of the given source object into a existing target object.
```C#
/* The source and target classes do not have to match or even be derived
   from each other, as long as the properties match. */
SomeType obj1 = new SomeType();
AnotherType obj2 = new AnotherType();
Copier.Copy(obj1, obj2);
```
