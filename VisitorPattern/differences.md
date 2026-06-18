# Visitor Pattern — C++ vs C# Key Differences

### 1. The Abstract Contract (Interfaces vs Pure Virtual Functions)
* **C++:** Relies on abstract classes with pure virtual functions matching zero (`virtual void visit(T* doc) = 0;`) to implement structural rules. Memory layout, visibility modifiers, and implementation overrides must be declared explicitly.
* **C#:** Implements native contracts using the `interface` keyword. Subclasses satisfy contracts directly without requiring explicit override tracking keywords or manual method declarations within the inheritance chain.

### 2. Forward Declarations vs Single-Pass Compilation
* **C++:** Requires explicit forward declarations for all concrete element classes (`class TextFile;`) at the top of the file because the visitor interface refers to element type parameters before their structural definitions are parsed.
* **C#:** Resolves type definitions non-sequentially across the assembly layout. Program structures can be declared in any visual sequence without utilizing header specifications or forward prototyping keywords.

### 3. Inheritance Accessibility Rules
* **C++:** Restricts type relationships to private inheritance access levels by default if visibility keys are omitted (`class TextFile : IDocument`). Explicit `public` descriptors are mandatory to support polymorphic runtime dispatches.
* **C#:** Maps implementation contracts openly using structural syntax rules (`public class TextFile : IDocument`). Realization definitions default to public access bindings automatically.

### 4. Memory Management & Lifecycle Declarations
* **C++:** Requires a virtual destructor chain (`virtual ~IDocument() = default;`) in base interfaces. Subclasses must override this destruction path using `override` keywords to prevent memory leaks during collection traversal routines.
* **C#:** Delegates object lifetime management entirely to automated Garbage Collection tracking. Explicit destructor logic is omitted across intermediate object graphs and polymorphic implementation boundaries.

### 5. Dispatch Argument Typing
* **C++:** Processes operational addresses using raw, explicit heap tracking pointers (`IDocumentVisitor* visitor`). Type safety measures depend on argument references being evaluated explicitly during compilation steps.
* **C#:** Employs managed object handles passed directly through reference semantics, eliminating manual variable reference tracking and memory addressing logic.

### 6. Operational Dispatches and Syntax Styles
* **C++:** Resolves element structures using separate pointer syntax blocks and explicit resolution qualifiers across class definitions.
* **C#:** Streamlines standard dispatch definitions using brief expression-bodied formats (`=> visitor.Visit(this);`), reducing structural boilerplate across model collections.

---

### Summary Table

| Feature | C++ Implementation | C# Implementation |
| :--- | :--- | :--- |
| **Abstraction Layer** | Pure Virtual Classes (`= 0`) | Native `interface` Declarations |
| **Forward Declarations** | Mandatory for argument definitions | Not required by assembly layout |
| **Inheritance Default** | Private access configuration | Open public interface realization |
| **Destructor Required?** | Yes, explicit `virtual` declarations | No, managed by Garbage Collector |
| **Parameter Types** | Raw typed pointers (`T* document`) | Managed type references (`T document`) |
| **Method Implementation** | Block syntax with override tracking | Expression-bodied arrows (`=>`) |
| **Collection Storage** | Vectors tracking pointers (`vector<T*>`) | Lists tracking interfaces (`List<T>`) |