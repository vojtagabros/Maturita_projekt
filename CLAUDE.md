# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Unity 2022.3.16f1 stealth/escape game (maturita graduation project). The player must escape a level by finding the exit, avoiding an enemy that pursues them when detected in light. They can pick up a weapon and call for help; if caught by the enemy a random fight is resolved.

## Building & Running

Open the project in Unity 2022.3.16f1. There are no CLI build commands — everything is done through the Unity Editor. Scripts are C# 9.0 targeting .NET 4.7.1.

## Scene Structure

Scene names used in `SceneManager.LoadScene()` (also loadable by index):
- `0` / `"MainMenu"` — main menu
- `1` / `"SampleScene"` — the game level
- `2` / `"ResultScene"` — result screen

## Architecture

### Game State (`GameData.cs`)
`GameData` is a `MonoBehaviour` with static fields. State is reset via `[RuntimeInitializeOnLoadMethod]` — a `sceneLoaded` handler resets all fields whenever `SampleScene` loads (plus in `Awake` as a fallback). All gameplay scripts write to it; `DisplayInfo` reads it on the result screen.

| Field | Type | Set by | Meaning |
|---|---|---|---|
| `Escaped` | bool | `Exit.cs`, `PlayerDeath.cs` (fight win) | Player reached exit or won fight |
| `Died` | bool | `PlayerDeath.cs` | Player lost the fight |
| `PlayerSeen` | bool | `EnemyVisibility.cs` | Enemy spotted the player (latching) |
| `WeaponFound` | bool | `Phone.cs` (PhonePickup) | Player picked up the weapon item |
| `HelpCalled` | float | `TimeManagement.cs` | Elapsed seconds when Call Help was clicked (0 = not called) |
| `FoughtAttacker` | bool | `PlayerDeath.cs` | Enemy physically collided with player |
| `FightWon` | bool | `PlayerDeath.cs` | Player won the random fight |
| `SurvivalTime` | float | `Exit.cs`, `PlayerDeath.cs` | Elapsed seconds at game end |
| `GameStartTime` | float | `GameData.Awake` | `Time.time` snapshot at scene load |
| `ExitFound` | float | (unused) | — |

### Scoring (`DisplayInfo.cs`)
Success base: 500 pts (300 if `FoughtAttacker`). Bonuses: time bonus `max(0, 300 - floor(SurvivalTime)*2)`, +100 if not detected, +75 if help not called. Failure: survival bonus `min(200, floor(SurvivalTime)*2)`. Grades: A≥850, B≥700, C≥500, D≥300, else F.

### Enemy Detection Pipeline
`PlayerLightDetector.cs` (class `AreaLightDetector`) runs `Physics.OverlapSphere` around a light source each frame. If the enemy is within range and no obstacle blocks the raycast, it calls `EnemyVisibility.SetVisible(true)`, which enables the enemy renderer and permanently calls `AIMovement.OnPlayerSeen()` — switching the enemy from wandering to chasing.

### Fight System
When the enemy collides with the player (`PlayerDeath.OnCollisionEnter`), a fight is resolved once: 1/3 chance of winning with weapon, 1/5 without. Win → `Escaped=true`, `FightWon=true`; loss → `Died=true`. Either way loads `"ResultScene"`.

### Player Interaction
- **Movement**: `PlayerMovement.cs` (class `RayVisualizer`) — click on Ground layer to NavMesh-navigate.
- **Grab**: `PlayerDrag.cs` — `Space` down/up creates/destroys a `SpringJoint` to a nearby object. Grabbed objects tracked in static `PlayerDrag.GrabbedObjects`.
- **Throw**: `Throw.cs` — `E` key applies impulse to all grabbed objects.

### Weapon/Phone Pickup
`PhoneSpawn.cs` (class `PhoneSpawner`) spawns 3 prefabs at random spawn points on `Start`. `Phone.cs` (class `PhonePickup`) sets `GameData.WeaponFound = true` and destroys itself on player trigger contact. (The file/class are named "Phone" for historical reasons — the item is now a weapon.)

### Key Class/File Name Mismatches
- `PlayerMovement.cs` → class `RayVisualizer`
- `PlayerLightDetector.cs` → class `AreaLightDetector`
- `Phone.cs` → class `PhonePickup`
- `PhoneSpawn.cs` → class `PhoneSpawner`

## Key Packages

- `com.unity.ai.navigation` — NavMesh for AI and player movement
- `com.unity.textmeshpro` — UI text in result screen
- `com.unity.probuilder` — Level geometry
- `com.unity.visualscripting` — Available but scripts use C# directly
