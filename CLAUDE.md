# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Unity 2022.3.16f1 stealth/escape game (maturita graduation project). The player must escape a level by finding the exit, avoiding an enemy that pursues them when detected in light. They can pick up phones and call for help.

## Building & Running

Open the project in Unity 2022.3.16f1. There are no CLI build commands — everything is done through the Unity Editor. Scripts are C# 9.0 targeting .NET 4.7.1.

## Scene Structure

Scene indices used in `SceneManager.LoadScene()`:
- `0` — MainMenu
- `1` — SampleScene (the game level)
- `2` — ResultScene

## Architecture

### Game State (`GameData.cs`)
`GameData` is a static `MonoBehaviour` that holds cross-scene state. All other scripts write to it; `DisplayInfo` reads it on the result screen.

| Field | Set by | Meaning |
|---|---|---|
| `Escaped` | `Exit.cs` | Player reached exit trigger |
| `Died` | `PlayerDeath.cs` | Enemy collided with player |
| `PlayerSeen` | `EnemyVisibility.cs` | Enemy spotted the player |
| `PhoneFound` | `Phone.cs` (PhonePickup) | Player picked up a phone |
| `HelpCalled` | `TimeManagement.cs` | Timestamp when player clicked Call Help |
| `ExitFound` | (unused so far) | — |

### Enemy Detection Pipeline
`PlayerLightDetector.cs` (class `AreaLightDetector`) runs `Physics.OverlapSphere` around a light source each frame. If an enemy is within range and no obstacle blocks the raycast, it calls `EnemyVisibility.SetVisible(true)`, which enables the enemy renderer and calls `AIMovement.OnPlayerSeen()` — permanently switching the enemy from wandering to chasing.

### Player Interaction
- **Movement**: `PlayerMovement.cs` (class `RayVisualizer`) — click on Ground layer to NavMesh-navigate.
- **Grab**: `PlayerDrag.cs` — `Space` down/up creates/destroys a `SpringJoint` connecting a nearby object to the player rigidbody. Grabbed objects are tracked in the static `PlayerDrag.GrabbedObjects` set.
- **Throw**: `Throw.cs` — `E` key iterates `PlayerDrag.GrabbedObjects` and applies impulse force.

### Phone System
`PhoneSpawn.cs` (class `PhoneSpawner`) spawns 3 phone prefabs at random positions from its configured spawn point list on `Start`. `Phone.cs` (class `PhonePickup`) destroys all objects tagged `"phone"` on player contact and sets `GameData.PhoneFound`.

### Key Class/File Name Mismatches
Several files have class names that differ from the filename:
- `PlayerMovement.cs` → class `RayVisualizer`
- `PlayerLightDetector.cs` → class `AreaLightDetector`
- `Phone.cs` → class `PhonePickup`
- `PhoneSpawn.cs` → class `PhoneSpawner`

## Key Packages

- `com.unity.ai.navigation` — NavMesh for AI and player movement
- `com.unity.textmeshpro` — UI text in result screen (`DisplayInfo.cs`)
- `com.unity.probuilder` — Level geometry
- `com.unity.visualscripting` — Available but scripts use C# directly
