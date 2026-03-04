# CSCI6617 Unity Game Project

## Overview

This repository contains a Unity 3D game project created for **CSCI6617**.  
The player navigates hazard-filled levels, avoids moving/animated obstacles, and reaches the goal to advance.

## Project Information

- **Course:** CSCI6617
- **Engine:** Unity `6000.3.5f2`
- **Platform:** macOS/Windows (Unity Editor)
- **Scenes in Build Order:**
  1. `Main` (main menu)
  2. `Level1`
  3. `Level2`

## Gameplay Summary

- Start from `Main` menu and choose a level.
- Move the player through each level while avoiding hazards.
- Touching hazards kills the player and respawns after a short delay.
- Reaching the goal loads the next scene.
- After the last level, the game returns to `Main`.

## Controls

- **Move:** `W A S D` or Arrow keys
- **Pause/Unpause:** `Esc`

## Core Systems Implemented

- **Player controller** (`CharacterController` based)
  - Acceleration/deceleration movement
  - Gravity and ground stick
  - Facing-direction model rotation
  - Respawn system on death
- **Hazard system**
  - Generic hazard trigger (`Hazard.cs`)
  - Animated spike trap with timed raise/lower states (`SpikeTrap.cs`)
- **Enemy/Obstacle behaviors**
  - Waypoint patroller (`Patroller.cs`)
  - Region-based wanderer (`Wanderer.cs` + `WanderRegion.cs`)
  - Shooter that spawns projectiles (`Shooter.cs` + `Projectile.cs`)
- **Progression/UI**
  - Goal trigger scene progression (`Goal.cs`)
  - Main menu scene loading and quit (`MainMenu.cs`)
  - Pause menu + time scale control (`PauseMenu.cs`)
- **Camera**
  - Follow camera with selectable view modes (`CameraFollow.cs`)

## Scripts Directory

All custom gameplay scripts are in:

- `Assets/Scripts/`

Included scripts:

- `Player.cs`
- `CameraFollow.cs`
- `Goal.cs`
- `Hazard.cs`
- `SpikeTrap.cs`
- `Patroller.cs`
- `Shooter.cs`
- `Projectile.cs`
- `WanderRegion.cs`
- `Wanderer.cs`
- `MainMenu.cs`
- `PauseMenu.cs`
- `ComponentColorUtility.cs`

## How to Run

1. Open the project in Unity Hub using Unity version `6000.3.5f2`.
2. Open scene `Assets/Scenes/Main.unity`.
3. Press **Play** in the Unity Editor.
4. Use menu buttons to start `Level1` or `Level2`.

## Build/Play Notes

- Build scene indexes are configured as:
  - `0 = Main`
  - `1 = Level1`
  - `2 = Level2`
- Ensure the player object uses the `Player` layer for trigger interactions.

## Academic Note

This project was developed as coursework for CSCI6617 to demonstrate Unity gameplay programming concepts including movement physics, AI behaviors, hazards, and scene management.
