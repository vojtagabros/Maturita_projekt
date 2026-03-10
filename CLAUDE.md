# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Unity 2022.3.16f1 stealth/escape game (maturita/graduation project). The player must escape a level by finding the exit, avoiding an enemy that pursues them when detected in light. They can pick up phones and call for help. If the enemy catches the player, a probabilistic fight determines survival.

## Building & Running

Open the project in Unity 2022.3.16f1. There are no CLI build commands — everything is done through the Unity Editor. Scripts are C# 9.0 targeting .NET 4.7.1.

## Scene Structure

Scene indices used in `SceneManager.LoadScene()`:
- `0` — MainMenu
- `1` — SampleScene (the game level)
- `2` — ResultScene

## Architecture

### Game State (`GameData.cs`)

`GameData` is a static `MonoBehaviour` that holds cross-scene state. All other scripts write to it; `DisplayInfo` reads it on the result screen. Fields are reset in `Awake()`.

| Field | Type | Set by | Meaning |
|---|---|---|---|
| `Escaped` | bool | `Exit.cs` | Player reached exit trigger (or won fight) |
| `Died` | bool | `PlayerDeath.cs` | Player lost the fight against enemy |
| `PlayerSeen` | bool | `EnemyVisibility.cs` | Enemy spotted the player in light |
| `PhoneFound` | bool | `Phone.cs` (PhonePickup) | Player picked up a phone |
| `HelpCalled` | float | `TimeManagement.cs` | Relative timestamp when player clicked Call Help (seconds from game start) |
| `FoughtAttacker` | bool | `PlayerDeath.cs` | Enemy collided with player (fight triggered) |
| `FightWon` | bool | `PlayerDeath.cs` | Player survived the fight |
| `GameStartTime` | float | `GameData.Awake()` | Absolute `Time.time` when game scene loaded |
| `SurvivalTime` | float | `Exit.cs` / `PlayerDeath.cs` | Duration (seconds) from game start to outcome |
| `ExitFound` | float | (unused) | Reserved, not currently set |

### Enemy Detection Pipeline

`PlayerLightDetector.cs` (class `AreaLightDetector`) runs `Physics.OverlapSphere` around a light source every 0.15 seconds. If an enemy is within `maxDistance` (15 units) and no obstacle blocks the raycast, it calls `EnemyVisibility.SetVisible(true)`, which:
1. Enables the enemy mesh renderer
2. Sets `GameData.PlayerSeen = true` (one-time, via `_alreadySeen` flag)
3. Calls `AIMovement.OnPlayerSeen()` — permanently switching the enemy from wandering to chasing

### Enemy AI (`AIMovement.cs`)

Two-state state machine:
- **Wandering:** Coroutine (`WanderRoutine`) cycles through `destinations[]` array with a 5-second pause at each waypoint
- **Chasing:** `OnPlayerSeen()` stops the coroutine and sets `hasDetectedPlayer = true`; `Update()` then continuously sets destination to player's position

NavMeshAgent settings: speed 5.5, acceleration 20, angularSpeed 720, stoppingDistance 0.2.

### Fight Mechanic (`PlayerDeath.cs`)

`OnCollisionEnter` on the enemy triggers when enemy hits player:
- Sets `GameData.FoughtAttacker = true`
- Win probability: **1/3 with phone**, **1/5 without phone**
- Win → `GameData.FightWon = true`, `GameData.Escaped = true`, load ResultScene
- Loss → `GameData.Died = true`, load ResultScene

### Player Interaction

- **Movement**: `PlayerMovement.cs` (class `RayVisualizer`) — left-click on Ground layer to NavMesh-navigate. Player spawns at a random position from `spawnPoints[]` on Start. NavMeshAgent: speed 4.0, acceleration 20, angularSpeed 720, stoppingDistance 0.2.
- **Grab**: `PlayerDrag.cs` — `Space` down/up creates/destroys a `SpringJoint` connecting a nearby object (within 2.5 units) to the player rigidbody. Grabbed objects tracked in static `PlayerDrag.GrabbedObjects` HashSet.
- **Throw**: `Throw.cs` — `E` key iterates `PlayerDrag.GrabbedObjects`, disconnects joints, and applies 5-unit impulse force to each object.

### Phone System

`PhoneSpawn.cs` (class `PhoneSpawner`) spawns 3 phone prefabs at random non-repeating positions from its `spawnPoints[]` list on `Start`. `Phone.cs` (class `PhonePickup`) destroys the contacted object on player trigger enter and sets `GameData.PhoneFound = true`.

**Note:** Phone pickup only sets `PhoneFound` on the first phone touched; subsequent phone objects in the scene are already destroyed by the `Destroy(gameObject)` call. The tag `"phone"` is used on phone prefabs.

### Scoring System (`DisplayInfo.cs`)

Score is computed in `CalculateScore()` on the result screen:

**Success (Escaped):**
| Component | Points |
|---|---|
| Base | 500 |
| Time bonus | +2 per second saved, up to +300 (150-second target) |
| Not detected | +100 |
| No help called | +75 |
| Phone found | +25 |

**Failure (Died):**
| Component | Points |
|---|---|
| Base | 0 |
| Survival time | +2 per second survived, up to +200 |
| Phone found | +25 |

**Grades:** A ≥ 850, B ≥ 700, C ≥ 500, D ≥ 300, F < 300

### Camera (`CameraFollow.cs`)

Locks the camera's rotation to its initial value every `LateUpdate()`. This prevents the camera from inheriting rotation changes from the player object it follows, maintaining a fixed orthographic-style perspective.

### UI Scripts

- **`DisplayInfo.cs`**: Result screen controller. Reads all `GameData` fields, shows either `FailCanvas` or `SuccesCanvas` (note typo in field name), populates TextMeshPro text fields with stats/score/grade. Provides `RestartGame()` and `GoToMenu()` button callbacks.
- **`TimeManagement.cs`**: Attaches `OnCallHelp()` to the Call Help button. Records `GameData.HelpCalled` as `Time.time - GameData.GameStartTime`; disables the button after use.
- **`ButtonManagement.cs`**: Generic utility — `Hide()` sets `gameObject.SetActive(false)`. Used for dismissable UI panels.
- **`SceneSwitcher.cs`**: Menu navigation — `LoadGameScene()` loads "SampleScene".

### Key Class/File Name Mismatches

Several files have class names that differ from the filename:

| Filename | Class Name |
|---|---|
| `PlayerMovement.cs` | `RayVisualizer` |
| `PlayerLightDetector.cs` | `AreaLightDetector` |
| `Phone.cs` | `PhonePickup` |
| `PhoneSpawn.cs` | `PhoneSpawner` |

When referencing these in Unity component search or `GetComponent<>` calls, use the **class name**, not the filename.

## Game Flow

1. **MainMenu (0):** Player clicks Play → `SceneSwitcher.LoadGameScene()` → SampleScene
2. **SampleScene (1):**
   - Player spawns at random position; enemy spawns at random position and begins patrol
   - Player navigates via click; grabs/throws objects with Space/E
   - Phone pickup available (improves fight odds)
   - Call Help button available (records timestamp, penalizes score)
   - Light detects enemy → enemy becomes visible and chases
   - **Win:** Reach exit trigger (`Exit.cs`) or win fight (`PlayerDeath.cs`) → ResultScene
   - **Lose:** Lose fight (`PlayerDeath.cs`) → ResultScene
3. **ResultScene (2):** `DisplayInfo.cs` reads `GameData`, shows outcome, score, grade; buttons restart or return to menu

## Key Packages

| Package | Version | Purpose |
|---|---|---|
| `com.unity.ai.navigation` | 1.1.6 | NavMesh for AI and player movement |
| `com.unity.textmeshpro` | 3.0.6 | UI text in result screen |
| `com.unity.probuilder` | 5.2.4 | Level geometry |
| `com.unity.visualscripting` | 1.9.1 | Available but unused (C# only) |
| `com.unity.timeline` | 1.7.6 | Available but unused |

## Development Conventions

- All custom scripts live in `Assets/Scripts/`
- Scene transitions use `SceneManager.LoadScene(int index)` with the indices above
- Cross-scene state lives exclusively in `GameData` static fields — never use `DontDestroyOnLoad` for data
- Detection is one-way and permanent: once `_alreadySeen` is set in `EnemyVisibility`, the enemy chases forever
- Physics layer masks `obstacleLayer` and `enemyLayer` must be correctly configured in the Inspector for detection to work
- The `PlayerDrag.GrabbedObjects` static set must be cleared between scenes if restarting (currently handled by `GameData.Awake()` resetting state, but the set itself persists — consider this if adding a restart-without-reload path)
