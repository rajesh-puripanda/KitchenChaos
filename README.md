# Kitchen Chaos

A fast-paced, chaotic top-down cooking game built from scratch in Unity. This project focuses heavily on production-ready software engineering principles, clean architectural decoupling, and scalable state management.

The core structural architecture of this project is based on the foundational design patterns taught by **Code Monkey**.

---

## Technical Overview & Architecture

### 1. Clean Software Architecture (Observer Pattern)
The game relies heavily on a decoupled event-driven system using **C# Events**. Components do not continuously poll each other for data; instead, visual elements, UI progress bars, and audio triggers listen to game-logic broadcasts. This ensures that the core gameplay mechanics remain completely independent of the presentation layer.

### 2. Strict State Management
Managing a dynamic real-time game requires solid state tracking. The game loop operates on a explicit state machine that transitions seamlessly through:
* **Waiting To Start / Tutorial Overlay**
* **Countdown Initialization**
* **Active Gameplay Loop**
* **Pause State**
* **Game Over Screen**

### 3. Precision Physics & Interaction Mechanics
Instead of relying on basic rigidbodies for movement that could slide out of control, custom physics vectors were tuned for crisp, grid-locked movement. Interaction radiuses and clear dot-product calculations ensure the player can accurately detect and target specific kitchen counters when holding or placing items.

---

## Visual Tour

### Main Menu
The entry point into the chaos. Handles scene transitions and dynamic loading.

![Main Menu](images/MainScreen.png)

### Control Guide & Tutorial
A responsive UI overlay that introduces mechanics and configures control mapping for both keyboard and gamepad.

![Interact Screen](images/InteractScreen.png)

### The Kitchen Grid (Gameplay)
The core arena featuring dynamic order queues, ingredient counters, cutting stations, stoves with visual burn-timers, and delivery bays.

![Game Level](images/GameScreen.png)

### Custom Collision Tuning
Behind the scenes testing for custom bounding volumes and continuous collision detection to ensure smooth player movement along kitchen edges.

![Collision Testing](images/collision.png)

---

## Raw Gameplay Footage

See the state machine, audio binding, and event system operational in real time during this test slice:

<video src="./images/KitchenChaos.mp4" width="100%" controls>
  Your browser does not support the video tag.
</video>
---

## How to Explore the Code
* Check `Assets/Scripts/Counters/` to see how different kitchen objects handle cooking, cutting, and plating logic through abstract base classes.
* Look at `Assets/Scripts/Player.cs` to analyze the movement vector math and interaction target raycasting.
* Browse `Assets/Scripts/GameStateMachine.cs` to evaluate the game cycle loop structure.

Feel free to dive into the codebase, audit the patterns, and tear the architecture apart.
