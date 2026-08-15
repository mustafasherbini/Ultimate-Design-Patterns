# Differences and Improvements

This file explains the differences between the implementations and documents the main improvements made during the development of the batch data processing pipeline.

---

## 1. Problem Summary

The problem describes a big data batch job that must check input data before processing it.

The required checks are:

```text
Validation Checks
Formatting Checks
Data Size Check
Personal Information Checks
```

The data should only be processed if all checks pass.

If any check fails, the pipeline must stop immediately.

---

## 2. Implemented Approaches

The project includes two main approaches:

```text
C++ : Classic Chain of Responsibility
C#  : Delegate-Based Pipeline
```

Both approaches solve the same problem and preserve the same behavior:

```text
Run checks in order.
Stop at the first failed check.
Process data only if all checks pass.
```

---

## 3. Classic Chain of Responsibility

The classic Chain of Responsibility version uses linked handlers.

Each handler knows the next handler in the chain.

Example:

```text
ValidationChecks -> FormattingChecks -> DataSizeCheck -> PersonalInformationChecks
```

Each handler is responsible for:

- Running its own check.
- Returning `false` if the check fails.
- Calling the next handler if the check passes.

---

## 4. Classic Chain Structure

The classic version uses these main components:

```text
Data
IHandler
BaseDataHandler
ValidationChecks
FormattingChecks
DataSizeCheck
PersonalInformationChecks
BatchJob
```

### `IHandler`

Defines the handler contract.

In the classic version, it usually contains:

```csharp
IHandler SetNext(IHandler nextHandler);

bool Handle(Data data);
```

### `BaseDataHandler`

Stores the next handler and contains the shared forwarding logic.

This avoids repeating the same next-handler code in every concrete handler.

### Concrete Handlers

The concrete handlers are:

```text
ValidationChecks
FormattingChecks
DataSizeCheck
PersonalInformationChecks
```

Each one performs one specific check.

---

## 5. BatchJob Decision

At first, we discussed whether `BatchJob` should exist in the UML and code.

Since the problem statement mentions a big data batch job, we decided to keep it.

`BatchJob` is not one of the checks.

Its responsibility is to:

```text
Start the pipeline.
Receive the pipeline result.
Process data if all checks pass.
Stop processing if any check fails.
```

So conceptually:

```text
BatchJob
   ↓
Pipeline
   ↓
Checks
   ↓
Process or Stop
```

---

## 6. C++ Version

The C++ version follows the classic Chain of Responsibility pattern.

It uses:

```text
IHandler
BaseDataHandler
Concrete handlers
BatchJob
Data
```

The handlers are linked together, and each handler can forward the request to the next one.

Example chain setup:

```cpp
validation
    .SetNext(&formatting)
    ->SetNext(&dataSize)
    ->SetNext(&personalInfo);
```

This version focuses on the traditional object-oriented implementation of Chain of Responsibility.

---

## 7. C# Classic Version

A C# classic Chain of Responsibility version was also discussed.

It follows the same idea as the C++ version, but uses C# syntax and managed references.

Example chain setup:

```csharp
validation
    .SetNext(formatting)
    .SetNext(dataSize)
    .SetNext(personalInfo);
```

This version helped clarify the classic design before moving to the delegate-based approach.

---

## 8. Delegate-Based Pipeline in C#

After the classic version, we explored solving the same problem using C# delegates.

The goal was to see if delegates can simplify the pipeline.

A delegate was introduced:

```csharp
public delegate bool DataPipelineDelegate(Data data);
```

This delegate represents any function that:

```text
Receives a Data object.
Returns true if the check passes.
Returns false if the check fails.
```

---

## 9. First Delegate-Based Version

The first delegate-based version manually called each handler.

```csharp
DataPipelineDelegate dataPipelineDelegate = data =>
{
    if (!validation.Handle(data)) return false;
    if (!formatting.Handle(data)) return false;
    if (!dataSize.Handle(data)) return false;
    if (!personalInfo.Handle(data)) return false;

    return true;
};
```

This version worked correctly.

It achieved:

```text
Run checks in order.
Stop on first failure.
Return true only if all checks pass.
```

However, it had one weakness:

```text
Adding a new check required editing the delegate body manually.
```

---

## 10. Improved Delegate-Based Pipeline

The delegate-based version was improved by storing all checks in a list.

```csharp
List<DataPipelineDelegate> checks = new List<DataPipelineDelegate>
{
    validation.Handle,
    formatting.Handle,
    dataSize.Handle,
    personalInfo.Handle
};
```

Then the pipeline became:

```csharp
DataPipelineDelegate dataPipelineDelegate = data =>
    checks.All(check => check(data));
```

This is cleaner and easier to extend.

---

## 11. Why `All` Was Used

LINQ `All` was used because it gives the exact behavior needed.

```csharp
checks.All(check => check(data));
```

This means:

```text
Run every check while the previous checks pass.
Stop immediately when a check returns false.
Return true only if all checks return true.
```

So if `FormattingChecks` fails, the next checks will not run.

Example:

```text
Running Validation Check...
Validation passed.
Running Formatting Check...
Formatting failed: unsupported format.
Pipeline failed. Data processing stopped.
```

`DataSizeCheck` and `PersonalInformationChecks` will not run in this case.

---

## 12. Main Improvement in the Delegate Version

Before improvement:

```csharp
if (!validation.Handle(data)) return false;
if (!formatting.Handle(data)) return false;
if (!dataSize.Handle(data)) return false;
if (!personalInfo.Handle(data)) return false;

return true;
```

After improvement:

```csharp
checks.All(check => check(data));
```

This improved the code by:

- Reducing repeated `if` statements.
- Making the pipeline easier to extend.
- Making the execution logic shorter.
- Keeping early termination behavior.
- Keeping each check independent from the others.

To add a new check now, we only add it to the list:

```csharp
checks.Add(newCheck.Handle);
```

No change is needed in the pipeline execution logic.

-

## 13. Classic Chain vs Delegate-Based Pipeline

### Classic Chain of Responsibility

In the classic version:

```text
Each handler knows the next handler.
```

Example:

```text
ValidationChecks -> FormattingChecks -> DataSizeCheck -> PersonalInformationChecks
```

The handler itself controls whether to continue to the next handler.

---

### Delegate-Based Pipeline

In the delegate-based version:

```text
Handlers do not know each other.
```

The pipeline order is controlled by:

```text
List<DataPipelineDelegate>
```

The checks are executed using:

```csharp
checks.All(check => check(data));
```

This makes the pipeline easier to extend.

To add a new check, add it to the list:

```csharp
checks.Add(newCheck.Handle);
```

---

### Summary

This project demonstrates the same batch checking problem using different styles.

The important behavior is preserved in all versions:

```text
Run checks in order.
Stop immediately when a check fails.
Process data only when all checks pass.
```

The classic Chain of Responsibility version focuses on linked handlers.

The delegate-based C# version focuses on a simple list of check functions.

---

## 14. Comparison Table

| Point | Classic Chain of Responsibility | Delegate-Based Pipeline |
|---|---|---|
| Pipeline control | Each handler calls the next handler | A list of delegates controls the order |
| Handler relationship | Handlers are linked | Handlers are independent |
| Needs `SetNext` | Yes | No |
| Needs `BaseDataHandler` | Usually yes | No |
| Execution style | Object chain | Delegate list |
| Early stop | Yes | Yes |
| Easy to add checks | Add handler and link it | Add handler to the list |
| Main focus | Design pattern structure | Simpler pipeline composition |

---