# ⚔️ Turn-Based Combat System (LinkedList Edition)

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET 8](https://img.shields.io/badge/.NET%208-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Status](https://img.shields.io/badge/Status-Completed-success?style=for-the-badge)

> **A tactical RPG battle simulation engineered with C# and Data Structures.**  
> This project demonstrates advanced logic by using a **Circular LinkedList** to manage dynamic turn rotations, replacing standard arrays for optimized state management.

---

## 📖 Overview

This console application simulates a battle between a **Hero** (Player) and procedurally generated **Enemies** (AI). The core challenge addressed in this project is efficient turn management. By utilizing a `LinkedList<Character>`, the system creates a "round-robin" loop where entities act in sequence, and defeated units are removed instantly in **O(1)** time complexity without disrupting the turn order.

---

## 🛠️ Tech Stack & Concepts

*   **Language:** C# (.NET 8)
*   **Architecture:** Object-Oriented Programming (OOP)
*   **Data Structures:** 
    *   `LinkedList<T>` 🔄: Used for the core battle queue and circular turn logic.
    *   `List<T>` 📜: Used for the "Graveyard" system (History Log).
*   **Key Techniques:**
    *   **Polymorphism:** Abstract `Character` base class with `Hero` and `Enemy` derivatives.
    *   **LINQ:** Efficient querying for target selection (`Where`, `Any`, `FirstOrDefault`).
    *   **Null Safety:** Modern C# practices to prevent runtime crashes.

---

## ✨ Key Features

### 🔄 Circular Turn System
Instead of a simple loop, the engine uses a pointer node (`LinkedListNode`) that traverses the list. When it reaches the end, it wraps back to the start (`Next ?? First`), creating an infinite game loop until a win/loss condition is met.

### 🎯 Dynamic Targeting Logic
*   **Hero Turn:** The player scans the battlefield. The system filters alive enemies using LINQ and presents a numbered selection menu.
*   **AI Turn:** Enemies automatically identify the Hero and execute attacks based on defined behavior.

### 💀 The Graveyard (Battle Log)
A unique feature that tracks casualties. When an enemy is defeated:
1.  Their node is removed from the active `LinkedList` (Combat).
2.  Their data object is archived in a `List<Character>` (Graveyard).
3.  Upon victory, a detailed report of all defeated enemies is displayed.

### 🎲 Procedural Generation
Every battle is unique. The system generates a random number of enemies (1-4) with randomized Health and Stats at the start of each session.

---

## 📂 Project Structure

```text
Turn_BasedCombat
│
├── Core
│   └── BattleSystem.cs    # The Engine: Manages LinkedList, Turns, and Game Loop.
│
├── Model
│   ├── Character.cs       # Abstract Base Class (Health, Damage, IsAlive).
│   ├── Hero.cs            # Player Logic.
│   └── Enemy.cs           # AI Logic.
│
└── Program.cs             # Entry Point.
