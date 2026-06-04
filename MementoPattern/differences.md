# Memento Pattern — C++ vs C# Key Differences

### 1. Encapsulation Styles (Friend Classes vs Nested Classes)
* **C++:** Uses the `friend` keyword. `CanvasMemento` can mark `class Canvas` as a friend, allowing `Canvas` to access its protected members while hiding them completely from `HistoryManager` and the outside world.
* **C#:** Does not have a `friend` keyword. Instead, it achieves the same "strict encapsulation" by nesting `CanvasMemento` as a `private class` inside `Canvas`.

### 2. The Abstract Contract (Interface vs Concrete Pointers)
* **C#:** Enforces a polymorphic contract using an explicit interface `IMemento`. The `HistoryManager` is entirely decoupled from the canvas logic, storing only generic `IMemento` references and passing them blindly.
* **C++:** Skips the interface layer entirely. Since memory addresses must be tracked tightly, `HistoryManager` works directly with the concrete type pointers (`const CanvasMemento*`). Encapsulation is preserved strictly via accessibility keywords rather than abstract types.

### 3. Inline Constructor Syntax
* **C++:** Achieved using **Member Initializer Lists** directly inside the class definition (e.g., `Canvas(string col) : color(col) {}`).
* **C#:** Achieved using **Expression-bodied members** and **Tuple deconstruction** (e.g., `public Canvas(string col) => color = col;`).

### 4. Memory Management & Pointers
* **C++:** History management relies on raw/const pointers (`const CanvasMemento*`). The Mementos are explicitly allocated on the **Heap** (`new`). If not cleaned manually, it causes a severe Memory Leak.
* **C#:** Relies on managed references. Objects are automatically handled by the **Garbage Collector (GC)**.

### 5. Explicit Resource Cleanup (Destructors)
* **C++:** Requires an explicit Destructor (`~HistoryManager()`) to clear both stacks and execute `delete` on all stored heap pointers, preventing lingering memory allocation.
* **C#:** Destructors are not needed for memory clearing since the collection framework clears internal references, automatically freeing memory via GC.

### 6. Const-Correctness
* **C++:** Strictly enforces data immutability. The `HistoryManager` stores `const CanvasMemento*` pointers, meaning the history log is unmodifiable. To delete a const pointer, explicit `const_cast` is needed inside the caretaker.
* **C#:** Does not natively support const variables for object instances (only for primitives/compile-time constants).

---

### Summary Table

| Feature | C++ Implementation | C# Implementation |
| :--- | :--- | :--- |
| **Abstraction Layer** | No Interface (Concrete Const Pointers) | Explicit `IMemento` Interface |
| **Strict Encapsulation** | `friend class` mechanism | Private nested classes |
| **Inline Constructor Style**| Member Initializer List (`: var(val)`) | Expression-bodied Tuple (`=> (a,b)=(x,y)`) |
| **History Storage Type** | Stacks of pointers (`stack<const T*>`) | Stacks of references (`Stack<IMemento>`) |
| **Destructor Required?** | Yes, to prevent Memory Leaks | No, fully automated by Garbage Collector |
| **Const Protection** | Supported natively via `const` pointers | Not supported for objects/references |
| **Casting Safety** | Manual `const_cast` for deletes | Pattern matching conditional (`is` keyword) |