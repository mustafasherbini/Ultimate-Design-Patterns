# Strategy Pattern

Implementation of the Strategy design pattern in C++ and C#.

## Pattern Overview

The Strategy pattern defines a family of algorithms, puts each in a separate class,
and makes them interchangeable — without changing the class that uses them.

## Structure

- **Context** — `NotificationService` holds a reference to `INotificationChannel` and delegates the work to it
- **Strategy Interface** — `INotificationChannel` defines the contract for all notification channels
- **Concrete Strategies** — `SMS`, `Email`, `Messenger`, `Slack` each implement their own sending logic
- **Data Class** — `User` holds the user's contact details

## Exercise

A notification service that supports multiple channels:
- The service doesn't know or care which channel is used
- Strategy is injected via constructor and can be switched at runtime
- Adding a new channel requires no changes to `NotificationService`

## UML Diagram

![Strategy Pattern UML](uml.png)

## Files

    cpp/strategy.cpp       → C++ implementation
    csharp/Strategy.cs     → C# implementation
    differences.md         → Key differences between C++ and C#

## Key Concepts Demonstrated

- Strategy injected via constructor — loose coupling
- Strategy switchable at runtime via `setChannel()`
- Open/Closed Principle — add new channels without modifying `NotificationService`
- Dependency on abstraction (`INotificationChannel`), not concrete classes
