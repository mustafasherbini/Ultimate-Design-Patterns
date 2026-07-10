# Iterator Design Pattern

Implementation of the Iterator design pattern in C++ and C#.

## Pattern Overview

The Iterator pattern is a behavioral design pattern that provides a way to access elements of a collection sequentially without exposing the collection's internal structure.

It separates traversal logic from the collection itself, allowing different traversal strategies to be implemented independently.

In this exercise, the pattern is used to iterate through social media profiles such as friends, family members, and mutual friends.

## UML

<img width="3125" height="2031" alt="gemini-svg (2)" src="https://github.com/user-attachments/assets/8863589a-f321-4bd0-a178-a3c0f21d3ba0" />

## Structure

### Iterator Interface

`IProfileIterator`

Declares the common operations used to traverse profiles.

It provides:

- `HasNext()`
- `GetNext()`

### Concrete Iterators

`FriendsIterator`

Iterates through all friends of a profile in their stored order.

`FamilyIterator`

Iterates only through friends marked as family members.

`MutualFriendsIterator`

Iterates through profiles that exist in both users' friends lists.

### Aggregate Interface

`IProfileIterable`

Declares methods responsible for creating profile iterators.

It provides methods for:

- Getting friends iterator
- Getting family iterator
- Getting mutual friends iterator

### Concrete Aggregate

`SocialMedia`

Creates and returns the correct iterator depending on the requested relationship type.

### Model

`Profile`

Represents a social media user profile.

Each profile contains:

- Name
- URL
- Friends list
- Family relationship map

## Exercise

A social media profile traversal system where:

- Users can iterate through friends.
- Users can iterate through family members.
- Users can find mutual friends between two profiles.
- Dashboard and analytics components can reuse the same iterator interface.
- Different traversal logic is hidden behind a common contract.

## Key Concepts Demonstrated

### Encapsulation

The client does not need to know how profiles are stored internally.

It only interacts with the iterator interface.

### Single Responsibility Principle

Each iterator class has one clear responsibility.

`FriendsIterator` handles friends traversal.

`FamilyIterator` handles family filtering.

`MutualFriendsIterator` handles mutual friends traversal.

### Open/Closed Principle

New traversal strategies can be added without changing existing client code.

For example, we can later add:

- BlockedUsersIterator
- CloseFriendsIterator
- SuggestedFriendsIterator

### Polymorphism

The client works with `IProfileIterator` without depending on concrete iterator classes.

## Iterator Types

### FriendsIterator

Returns all friends of a profile.

### FamilyIterator

Returns only friends marked as family members.

### MutualFriendsIterator

Returns friends that are shared between two profiles.
