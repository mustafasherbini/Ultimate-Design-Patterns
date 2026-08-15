# Big Data Batch Pipeline Checks

This project demonstrates how to solve a big data batch checking problem using two approaches:

1. Classic Chain of Responsibility Pattern
2. Delegate-Based Pipeline in C#

The goal is to process input data only after it passes a sequence of required checks.

---

## Problem Overview

We are developing a big data batch job.

Before processing the data, the data must pass the following checks:

```text
Validation Checks
        ↓
Formatting Checks
        ↓
Data Size Check
        ↓
Personal Information Checks
        ↓
Processing
```

If any check fails, the pipeline stops immediately and the data is not processed.

---

## Checks Included

The pipeline contains four checks:

### 1. Validation Checks

Checks whether the data content is empty or invalid.

### 2. Formatting Checks

Checks whether the data format is supported.

Supported formats:

```text
CSV
JSON
```

### 3. Data Size Check

Checks whether the data size is within the allowed maximum limit.

### 4. Personal Information Checks

Checks whether the data contains personal or sensitive information.

---

## Project Files

Suggested file structure:

```text
README.md
CHANGES.md

main.cpp
Program.cs
DelegateBatchPipeline.cs

chain-of-responsibility-uml.svg
delegate-based-pipeline-uml.svg
```

---

## Implementations

## 1. C++ Classic Chain of Responsibility

File:

```text
main.cpp
```

The C++ version implements the classic Chain of Responsibility pattern.

It uses:

- `Data`
- `IHandler`
- `BaseDataHandler`
- `ValidationChecks`
- `FormattingChecks`
- `DataSizeCheck`
- `PersonalInformationChecks`
- `BatchJob`

In this version, each handler can forward the request to the next handler in the chain.

Example chain:

```text
ValidationChecks
        ↓
FormattingChecks
        ↓
DataSizeCheck
        ↓
PersonalInformationChecks
```

The `BatchJob` starts the pipeline and decides whether the data should be processed.

---

## 2. C# Classic Chain of Responsibility

File:

```text
Program.cs
```

The C# classic version also follows the Chain of Responsibility pattern.

It uses an interface and a base handler class:

```csharp
public interface IHandler
{
    IHandler SetNext(IHandler nextHandler);

    bool Handle(Data data);
}
```

The shared forwarding logic is placed inside:

```text
BaseDataHandler
```

Each concrete handler overrides `Handle`.

If the check passes, it forwards the data to the next handler.

If the check fails, it returns `false` and stops the chain.

---

## 3. C# Delegate-Based Pipeline

File:

```text
DelegateBatchPipeline.cs
```

This version solves the same problem using C# delegates.

Instead of making each handler point to the next handler, the checks are stored as delegates in a list.

The delegate type is:

```csharp
public delegate bool DataPipelineDelegate(Data data);
```

Each check still implements:

```csharp
public interface IHandler
{
    bool Handle(Data data);
}
```

Then the handlers are added to a list of delegates:

```csharp
List<DataPipelineDelegate> checks = new List<DataPipelineDelegate>
{
    validation.Handle,
    formatting.Handle,
    dataSize.Handle,
    personalInfo.Handle
};
```

The pipeline is executed using:

```csharp
DataPipelineDelegate dataPipelineDelegate = data =>
    checks.All(check => check(data));
```

This keeps the behavior simple:

```text
Run checks in order.
Stop at the first failed check.
Return true only if all checks pass.
```

---

## BatchJob

`BatchJob` represents the batch process that runs the checking pipeline.

It is not one of the checks.

Its responsibility is to:

1. Start the pipeline.
2. Receive the pipeline result.
3. Process the data if all checks pass.
4. Stop processing if any check fails.

Conceptually:

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

## UML Diagrams

Two UML diagrams were created.

### Classic Chain of Responsibility UML

<img width="2763" height="1771" alt="Screenshot 2026-08-15 125646" src="https://github.com/user-attachments/assets/752ac538-ad92-40f0-8ad2-246a8342479d" />


This UML represents the classic object-oriented version using:

- `IHandler`
- `BaseDataHandler`
- Concrete handlers
- `BatchJob`
- `Data`

### Delegate-Based Pipeline UML


<img width="2836" height="1784" alt="image" src="https://github.com/user-attachments/assets/abe5538c-c783-4ed8-a773-b56eab83b49f" />

```text
delegate-based-pipeline-uml.svg
```

This UML represents the C# delegate-based version using:

- `DataPipelineDelegate`
- `List<DataPipelineDelegate>`
- `IHandler`
- Concrete handlers
- `BatchJob`
- `Data`

---

## Main Classes

## Data

Represents the input object passed through the pipeline.

Properties:

```text
Info
Format
SizeInMB
ContainsPersonalInformation
```

---

## IHandler

Defines the contract for all checks.

In the delegate-based version:

```csharp
public interface IHandler
{
    bool Handle(Data data);
}
```

Each handler returns:

```text
true  = check passed
false = check failed
```

---

## ValidationChecks

Checks that the data content is not empty.

---

## FormattingChecks

Checks that the data format is either:

```text
CSV
JSON
```

---

## DataSizeCheck

Checks that the data size does not exceed the maximum allowed size.

Example:

```text
Maximum size = 100 MB
```

---

