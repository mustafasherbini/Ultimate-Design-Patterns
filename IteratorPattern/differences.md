# Iterator Pattern — C++ vs C# Key Differences

## 1. Abstract Contracts

### C++

C++ implements interfaces using abstract classes with pure virtual functions.

```cpp
class IProfileIterator {
public:
    virtual ~IProfileIterator() = default;

    virtual Profile* getNext() = 0;

    virtual bool hasNext() = 0;
};
```

The `= 0` syntax makes the method pure virtual.

That means derived classes must implement it.

### C#

C# has a native `interface` keyword.

```csharp
public interface IProfileIterator
{
    Profile? GetNext();

    bool HasNext();
}
```

C# interfaces are cleaner and do not require pure virtual syntax.

---

## 2. Virtual Destructors

### C++

C++ requires a virtual destructor in base classes used polymorphically.

```cpp
virtual ~IProfileIterator() = default;
```

This allows safe deletion through a base class pointer.

### C#

C# does not require destructors for interfaces.

Object lifetime is handled automatically by the Garbage Collector.

---

## 3. Raw Pointers vs Managed References

### C++

The C++ implementation uses raw pointers.

```cpp
Profile*
```

For example:

```cpp
vector<Profile*> friends;
```

This means the code must be careful about ownership and object lifetime.

### C#

The C# implementation uses managed references.

```csharp
List<Profile>
```

Objects are reference types by default when declared as classes.

The runtime manages memory automatically.

---

## 4. Manual Memory Management vs Garbage Collection

### C++

The `SocialMedia` class creates iterators using `new`.

```cpp
return new FriendsIterator(profile->friends);
```

The client must call `delete` after using the iterator.

```cpp
delete iterator;
```

If the client forgets to delete the iterator, a memory leak can happen.

### C#

The `SocialMedia` class also creates objects using `new`.

```csharp
return new FriendsIterator(profile.Friends);
```

However, the client does not need to manually delete them.

The Garbage Collector cleans up unused objects automatically.

---

## 5. Collections

### C++

C++ uses `vector` for dynamic arrays.

```cpp
vector<Profile*> friends;
```

### C#

C# uses `List<T>`.

```csharp
public List<Profile> Friends { get; set; } = new List<Profile>();
```

Both provide dynamic list behavior.

The main difference is that the C++ version stores raw pointers, while the C# version stores managed references.

---

## 6. Hash Maps

### C++

The C++ version uses `unordered_map`.

```cpp
unordered_map<Profile*, bool> IsFamily;
```

This is used to mark whether a friend is also a family member.

### C#

The C# version uses `Dictionary<TKey, TValue>`.

```csharp
Dictionary<Profile, bool> IsFamily
```

Both provide fast key-based lookup.

---

## 7. Hash Sets

### C++

The mutual friends logic uses `unordered_set`.

```cpp
unordered_set<Profile*> _secondProfileFriends;
```

This allows fast checking whether a profile exists in another user's friends list.

### C#

The C# version uses `HashSet<T>`.

```csharp
HashSet<Profile> _secondProfileFriends;
```

Both structures are used to optimize membership checks.

---

## 8. Null Handling

### C++

C++ uses `nullptr`.

```cpp
return nullptr;
```

This is returned when there is no next profile.

### C#

C# uses `null`.

```csharp
return null;
```

The C# version can also use nullable reference syntax.

```csharp
Profile?
```

This makes it clear that the method may return `null`.

---

## 9. Method Naming Conventions

### C++

C++ commonly uses camelCase for method names.

```cpp
getNext()
hasNext()
```

### C#

C# commonly uses PascalCase for method names.

```csharp
GetNext()
HasNext()
```

This follows standard C# naming conventions.

---

## 10. Field Naming and Properties

### C++

The C++ version may expose fields directly for simplicity.

```cpp
string name;
string Url;
```

### C#

The C# version usually uses properties.

```csharp
public string Name { get; set; }
public string Url { get; set; }
```

Properties are the standard way to expose data in C# classes.

---

## 11. Inheritance Syntax

### C++

C++ requires explicit public inheritance for polymorphic behavior.

```cpp
class SocialMedia : public IProfileIterable
```

If `public` is omitted, inheritance is private by default for `class`.

### C#

C# does not use access modifiers in the inheritance list the same way.

```csharp
public class SocialMedia : IProfileIterable
```

The class simply declares that it implements the interface.

---

## 12. Iterator Creation

### C++

The concrete aggregate returns raw pointers.

```cpp
IProfileIterator* GetProfileFriends(Profile* profile)
```

The caller receives a pointer and becomes responsible for deleting it.

### C#

The concrete aggregate returns managed interface references.

```csharp
IProfileIterator GetProfileFriends(Profile profile)
```

The caller does not manage deletion manually.

---

## 13. Family Iterator Logic

### C++

The `FamilyIterator` filters friends using an `unordered_map`.

```cpp
while (idx < _friends.size()) {
    Profile* currentProfile = _friends[idx];

    bool isFamilyMember =
        _isFamily.find(currentProfile) != _isFamily.end() &&
        _isFamily[currentProfile];

    if (isFamilyMember) {
        return true;
    }

    idx++;
}
```

### C#

The C# version uses `TryGetValue`.

```csharp
bool isFamilyMember =
    _isFamily.TryGetValue(currentProfile, out bool value) && value;
```

`TryGetValue` avoids accidental insertion of missing keys.

---

## 14. Mutual Friends Logic

### C++

The C++ version compares profile pointers.

```cpp
_secondProfileFriends.find(_firstProfileFriends[idx])
```

Two profiles are considered the same if they point to the same object in memory.

### C#

The C# version compares object references by default when using `HashSet<Profile>`.

This keeps behavior similar to the C++ pointer-based comparison.

If value-based equality is needed, `Equals()` and `GetHashCode()` should be overridden.

---

# Summary Table

| Feature | C++ Implementation | C# Implementation |
|---|---|---|
| Interface Style | Abstract class with pure virtual methods | Native `interface` |
| Iterator Return Type | `Profile*` | `Profile?` |
| List Type | `vector<Profile*>` | `List<Profile>` |
| Family Map | `unordered_map<Profile*, bool>` | `Dictionary<Profile, bool>` |
| Mutual Lookup | `unordered_set<Profile*>` | `HashSet<Profile>` |
| Null Value | `nullptr` | `null` |
| Memory Management | Manual `new` and `delete` | Garbage Collection |
| Base Cleanup | Virtual destructor required | Not required |
| Inheritance | `public` inheritance required | Interface implementation |
| Method Naming | `getNext()`, `hasNext()` | `GetNext()`, `HasNext()` |
| Data Exposure | Public fields possible | Properties preferred |
| Object Comparison | Pointer/reference comparison | Reference comparison by default |

---

# Final Note

Both versions implement the same Iterator Design Pattern.

The C++ version focuses on explicit pointer handling, manual memory management, and pure virtual contracts.

The C# version focuses on managed references, native interfaces, properties, and automatic memory cleanup.

The design goal is the same in both languages.

The client can iterate through friends, family members, and mutual friends using the same iterator interface.