

# Observer Pattern

Implementation of the Observer design pattern in C++ and C#.

## Pattern Overview

The Observer pattern defines a one-to-many dependency between objects.
When one object (Subject) changes state, all its dependents (Observers) are notified automatically.

## Structure

- **Subject** — holds a dictionary of events → list of subscribers, and notifies them
- **ISubscriber** — interface that all subscribers must implement
- **Subscriber** — concrete implementation of ISubscriber
- **EventType** — enum defining the available event types

## Exercise

A blog management system where:
- Authors can publish blog posts or weekly newsletters
- Subscribers can subscribe/unsubscribe to specific event types
- Each subscriber is notified only for events they subscribed to

<img width="1360" height="1160" alt="uml" src="https://github.com/user-attachments/assets/988e8211-aa1e-4331-8f91-f847a3720e98" />

## Files

    cpp/observer.cpp       → C++ implementation
    csharp/Observer.cs     → C# implementation
    differences.md         → Key differences between C++ and C#

## Key Concepts Demonstrated

- Dictionary-based event management (one list per event type)
- Interface/abstract class for loose coupling
- Subscribe and unsubscribe without modifying the Subject
- Open/Closed Principle — add new subscriber types without changing BlogManagement


