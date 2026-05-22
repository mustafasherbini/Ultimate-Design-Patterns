# Strategy Pattern — C++ vs C# Key Differences

## 1. Interface Definition

C++ has no `interface` keyword. Use abstract class with pure virtual functions (= 0).

C++:
    class INotificationChannel {
    public:
        virtual void send(User* user, const string& message) = 0;
        virtual ~INotificationChannel() = default;
    };

C#:
    public interface INotificationChannel {
        void Send(User user, string message);
    }

---

## 2. Public Inheritance

C++ defaults to private inheritance — must explicitly write `public`:

C++:
    class SMS : public INotificationChannel { ... }  // ✅ public required

C#:
    public class SMS : INotificationChannel { ... }  // no public/private for inheritance

---

## 3. Passing Parameters — Pointer vs Reference vs Value

C++ gives you control over how to pass objects:
    void send(User* user, const string& message)
    // User* — pointer for polymorphism / nullable
    // const string& — reference to avoid copy, read-only

C#:
    void Send(User user, string message)
    // User is a reference type — always passed by reference automatically
    // string is immutable — no need for const

---

## 4. Null Safety

C++ pointer can be null — must check manually:
    if (user == nullptr) return;

C# reference can also be null but compiler warns you (nullable reference types):
    if (user is null) return;

---

## 5. Memory Management

C++ — manual, risk of memory leak:
    INotificationChannel* channel = new SMS();
    delete channel; // must delete manually

Modern C++ — use smart pointers:
    auto channel = make_unique<SMS>(); // auto deleted

C# — Garbage Collector handles it:
    INotificationChannel channel = new SMS(); // GC cleans up automatically

---

## 6. Virtual Destructor

C++ requires virtual destructor in base class to avoid memory leak:
    virtual ~INotificationChannel() = default;

C# does not need this — GC handles cleanup automatically.

---

## 7. Switching Strategy at Runtime

Both support switching strategy at runtime via a setter:

C++:
    void setChannel(INotificationChannel* channel) {
        _channel = channel;
    }

C#:
    public void SetChannel(INotificationChannel channel) {
        _channel = channel;
    }

---

## Summary Table

| Feature                    | C++                              | C#                          |
|----------------------------|----------------------------------|-----------------------------|
| Interface keyword          | No — abstract class + = 0        | Yes — interface keyword     |
| Public inheritance         | Must write `public` explicitly   | No modifier needed          |
| Parameter passing          | Choose: value, &, or *           | Auto reference for objects  |
| Memory management          | Manual / smart pointers          | Automatic (GC)              |
| Virtual destructor         | Required                         | Not needed                  |
| Null check                 | nullptr                          | null / is null              |
| String interpolation       | cout << "[SMS] " << msg          | $"[SMS] {msg}"              |
