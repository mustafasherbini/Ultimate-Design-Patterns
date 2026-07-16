# Observer Pattern — C++ vs C# Key Differences

## 1. Interface Definition

C++ has no `interface` keyword. Use a class with pure virtual functions (= 0).

C++:
    class ISubscriber {
    public:
        virtual ~ISubscriber() = default;
        virtual void update(NotifyEvents event, string msg) = 0;
    };

C#:
    public interface ISubscriber {
        void Update(NotifyEvents notifyEvent, string msg);
    }

---

## 2. Pure Virtual ( = 0 ) vs Interface Methods

In C++, `= 0` makes a function pure virtual:
- The class becomes abstract (cannot be instantiated)
- Any subclass MUST override it
- Without `= 0`, the class is not abstract and override is optional

In C#, all methods in an `interface` are implicitly abstract.
No `= 0` needed.

---

## 3. Pointers vs References

C++ requires pointers to store polymorphic objects:
    map<NotifyEvents, vector<ISubscriber*>> eventSubscribers;
    void subscribe(NotifyEvents event, ISubscriber* person);

C# uses references — no pointers needed:
    Dictionary<NotifyEvents, List<ISubscriber>> _eventSubscribers;
    public void Subscribe(NotifyEvents notifyEvent, ISubscriber person);

---

## 4. Virtual Destructor

C++ requires a virtual destructor in base classes to avoid memory leaks
when deleting via a base pointer:
    virtual ~ISubscriber() = default;

C# does not need this — the Garbage Collector (GC) handles memory automatically.

---

## 5. Remove from List

C++ requires combining remove() + erase() (erase-remove idiom):
    list.erase(remove(list.begin(), list.end(), person), list.end());

C# has a built-in Remove():
    _eventSubscribers[notifyEvent].Remove(person);

---


## Summary Table

| Feature                  | C++                          | C#                        |
|--------------------------|------------------------------|---------------------------|
| Interface keyword        | No — use class + = 0         | Yes — interface keyword   |
| Pure virtual             | virtual void f() = 0         | All interface methods      |
| Polymorphic storage      | Pointers (ISubscriber*)      | References (ISubscriber)  |
| Virtual destructor       | Required                     | Not needed (GC)           |
| Remove from list         | erase-remove idiom           | .Remove() built-in        |
| Memory management        | Manual (new/delete)          | Automatic (GC)            |
