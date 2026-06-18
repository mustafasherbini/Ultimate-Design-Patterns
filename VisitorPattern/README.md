# Visitor Design Pattern

Implementation of the Visitor design pattern in C++ and C#.

## Pattern Overview
The Visitor pattern is a behavioral design pattern that allows adding new operations to an existing object structure without modifying the structures themselves. It achieves this by shifting operational logic into external visitor classes utilizing double-dispatch mechanics.
## UML
<img width="2500" height="1875" alt="gemini-svg" src="https://github.com/user-attachments/assets/8cb1981c-fe25-42a9-9000-ae92d26a5c40" />

## Structure
* **Element (`IDocument`)**: Declares an accept method that takes a visitor as an argument.
* **Concrete Elements (`TextFile`, `Spreadsheet`, `PresentationFile`)**: Objects implementing the element interface that pass themselves to the visitor's dispatch loop.
* **Visitor (`IDocumentVisitor`)**: Declares a distinct visit method for every concrete element type within the target object structure.
* **Concrete Visitors (`WordCounter`, `TextExtractor`, `FormatAnalyzer`)**: Implementations encapsulating specific cross-cutting operations executed across element collections.

## Exercise
A multi-format document analysis framework where:
1. Document types hold discrete domain representations but share structural execution baselines.
2. New operational behaviors are constantly attached without altering stable file handling code.
3. System components process nested lists polymorphically without resorting to type-casting or manual instance checks.

## Key Concepts Demonstrated
* **Double Dispatch Mechanism:** Resolves structural routing logic dynamically based on both the runtime type of the element and the visitor.
* **Open/Closed Principle:** Extends operational capabilities safely across all document subclasses without changing their source declarations.
* **Single Responsibility:** Separates algorithms from the core representation models they operate on.
