# Maturita Projekt — Stealth/Escape Game

Unity 2022.3.16f1 stealth game made as a graduation (maturita) project.

**Play in browser:** https://vojtagabros.github.io/Maturita_projekt/

## Gameplay

The player must escape a level by reaching the exit while avoiding an enemy. The enemy patrols the level and starts chasing when it detects the player standing in light. The player can pick up a weapon and call for help. If caught, a random fight is resolved — winning the fight also counts as an escape.

### Controls

| Action | Input |
|---|---|
| Move | Left click |
| Grab object | Hold Space |
| Release object | Release Space |

### Scoring

| Condition | Points |
|---|---|
| Success base | 500 pts (300 if fought the enemy) |
| Time bonus | max(0, 300 − elapsed seconds × 2) |
| Not detected | +100 |
| Help not called | +75 |
| Failure survival bonus | min(200, elapsed seconds × 2) |

Grades: A ≥ 850, B ≥ 700, C ≥ 500, D ≥ 300, F otherwise.

## Scenes

- **MainMenu** — main menu
- **SampleScene** — the game level
- **ResultScene** — result screen with score and grade

## Project Structure

```
Assets/
  Scripts/       — all C# gameplay scripts
  Scenes/        — Unity scene files
  Materials/     — materials for level geometry
  ProBuilder Data/
```

### Key Scripts

| File | Class | Role |
|---|---|---|
| `GameData.cs` | `GameData` | Static game state holder |
| `AIMovement.cs` | `AIMovement` | Enemy NavMesh AI (wander → chase) |
| `PlayerMovement.cs` | `RayVisualizer` | Click-to-move via NavMesh |
| `PlayerLightDetector.cs` | `AreaLightDetector` | Detects enemy in light via OverlapSphere + raycast |
| `EnemyVisibility.cs` | `EnemyVisibility` | Triggers enemy chase on detection |
| `PlayerDeath.cs` | `PlayerDeath` | Resolves fight on enemy collision |
| `DisplayInfo.cs` | `DisplayInfo` | Shows result screen with score |
| `Exit.cs` | `Exit` | Triggers escape on exit contact |

## Building

Open the project in Unity 2022.3.16f1. Use **File → Build Settings** to build. Scripts target C# 9.0 / .NET 4.7.1.
