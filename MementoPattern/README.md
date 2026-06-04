# Memento Design Pattern

Implementation of the Memento design pattern in C++ and C#.

## Pattern Overview
The Memento pattern is a behavioral design pattern that allows capturing and externalizing an object's internal state without violating encapsulation, so that the object can be restored to this state later (Undo/Redo mechanics).

## Structure
* **Originator (`Canvas`)**: The actual application object whose state needs saving/restoring.
* **Memento (`CanvasMemento`)**: A snapshot carrier containing the state of the Canvas. It is strictly encapsulated from outside modifications.
* **Caretaker (`HistoryManager`)**: Safely stores the timeline sequence of Memento snapshots without interacting with the data inside them.

## Exercise
A Canvas state tracker where:
1. The canvas state holds drawing data (`color`, `content`, `border`).
2. Attributes can be safely modified without altering object identity references.
3. Boundary guards prevent system crashes when executing illegal sequential Undos or Redos.

## Files
* `cpp/memento.cpp`       → C++ implementation with secure Pointer and Destructor management.
* `csharp/Memento.cs`     → C# implementation featuring Private Nested types and Expression-bodied rules.
* `differences.md`         → Detailed architectural differences between C++ and C# regarding this pattern.

## Key Concepts Demonstrated
* **Encapsulation Protection:** Caretaker (`HistoryManager`) cannot read or temper with the saved values inside snapshots.
* **Boundary Checks:** System returns `null`/`nullptr` when trying to step outside the timeline boundary, keeping the object state safe.
* **Memory Optimization:** Inline constructors used for optimized register-level object building blocks.