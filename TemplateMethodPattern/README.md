# Template Method Pattern

Implementation of the Template Method design pattern in C++ and C#.

## Pattern Overview

The Template Method pattern defines the skeleton of an algorithm in a base class while allowing subclasses to redefine specific steps without changing the overall workflow.

In this project:
- The video editing workflow is fixed
- Only some steps (color correction and rendering) vary depending on the preset quality

---

## Structure

### VideoEditingPresets
Abstract base class that:
- Defines the full processing workflow using `Process()`
- Implements common shared steps:
  - Enhance video quality
  - Enhance audio quality
  - Apply filters
- Declares customizable steps:
  - Apply color correction
  - Render video

### FHDVideoPresets
Concrete preset for Full HD rendering.

### HDVideoPresets
Concrete preset for HD rendering.

### SDVideoPresets
Concrete preset for SD rendering.

---

## Exercise

A video editing preset system where:
- All video presets follow the same editing pipeline
- Different presets customize:
  - Color correction
  - Rendering quality
- Users can choose between:
  - FHD
  - HD
  - SD

---

## Files

cpp/template_method.cpp      → C++ implementation
csharp/TemplateMethod.cs     → C# implementation
differences.md               → Key differences between C++ and C#

---

## Key Concepts Demonstrated

- Template Method Pattern
- Abstract base class
- Polymorphism
- Method overriding
- Reusable workflow logic
- Open/Closed Principle:
  - Add new video presets without modifying existing workflow
- Separation between fixed behavior and customizable behavior

---

## Workflow Steps

1. Enhance Video Quality
2. Apply Color Correction
3. Enhance Audio Quality
4. Apply Filters
5. Render Video

The workflow order is fixed inside the Template Method (`Process()`), while subclasses customize only selected steps.