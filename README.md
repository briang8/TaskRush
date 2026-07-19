# TaskRush

A top-down infiltration mini-game built in Unity. The player enters a room and must complete four tasks, eliminate guards, collect two items, and crack a terminal code, before a timer runs out or an enemy kills them.

## Setup Instructions

1. Clone or download this repository.
2. Open the project folder in Unity Hub. This project was built on Unity 6 (6000.0.75f1).
3. Open the scene at `Assets/TopDown_MiniGame/Main.unity`.
4. Press Play to run the game in the editor.

## Controls

* WASD or arrow keys to move
* Mouse to aim
* Left mouse button to attack
* 1 to switch to knife, 2 to switch to pistol
* E to interact with the terminal
* R to restart after the game ends
* Q to quit after the game ends

## Project Structure

* `Assets/TopDown_MiniGame/Scripts` contains all gameplay scripts.
* `Assets/TopDown_MiniGame/Scripts/NPC` contains the enemy AI, sensors, and patrol node scripts.
* `Assets/TopDown_MiniGame/Prefabs` contains the player, enemy, and projectile prefabs.
* `Assets/TopDown_MiniGame/Scenes/Main.unity` is the main playable scene.

## Code Documentation

Scripts are commented to highlight where OOP principles, design patterns, and algorithms are used. For a full written explanation, see the accompanying documents:

* https://docs.google.com/document/d/1FTgPZOLh5-5PfIbHw5OZOORyjvIOCgkBH3wfJHqlZbk/edit?tab=t.0
## Design Patterns Used

* Singleton, in GameManager.cs
* Observer, between GameManager.cs and GameUI.cs
* State, in NPC_Enemy.cs

## Algorithms Implemented

* State-based decision logic for enemy AI, in NPC_Enemy.cs
* Waypoint patrol using linked-list traversal, in NPC_PatrolNode.cs and NPC_Enemy.cs