# Maturita Projekt — Stealth/Escape Game

Unity 2022.3.16f1 stealth hra vytvořená jako maturitní projekt.

**Hrát v prohlížeči:** https://vojtagabros.github.io/Maturita_projekt/

**Teoretická část práce:** https://docs.google.com/document/d/1GJBLvGKVKsis_RNstjtR9hNV3VpBbZZQpGoVj2nr0eo/edit?usp=sharing

## Gameplay

Hráč musí uniknout z úrovně dosažením východu a zároveň se vyhýbat nepříteli. Nepřítel hlídkuje po úrovni a začne pronásledovat hráče, jakmile ho spatří ve světle. Hráč může sebrat zbraň a zavolat pomoc. Pokud ho nepřítel dostihne, rozhodne se náhodný souboj — výhra v souboji se také počítá jako útěk.

### Ovládání

| Akce | Vstup |
|---|---|
| Pohyb | Levé tlačítko myši |
| Chytit objekt | Držet mezerník |
| Pustit objekt | Pustit mezerník |

### Bodování

| Podmínka | Body |
|---|---|
| Základ za úspěch | 500 bodů (300 pokud došlo k souboji) |
| Časový bonus | max(0, 300 − uplynulé sekundy × 2) |
| Nebyl odhalen | +100 |
| Nepožádal o pomoc | +75 |
| Bonus za přežití (neúspěch) | min(200, uplynulé sekundy × 2) |

Známky: A ≥ 850, B ≥ 700, C ≥ 500, D ≥ 300, jinak F.

## Scény

- **MainMenu** — hlavní menu
- **SampleScene** — herní úroveň
- **ResultScene** — obrazovka výsledků se skóre a známkou

## Struktura projektu

```
Assets/
  Scripts/       — všechny C# herní skripty
  Scenes/        — soubory Unity scén
  Materials/     — materiály pro geometrii úrovně
  ProBuilder Data/
```

### Klíčové skripty

| Soubor | Třída | Role |
|---|---|---|
| `GameData.cs` | `GameData` | Uchovává stav hry |
| `AIMovement.cs` | `AIMovement` | NavMesh AI nepřítele (hlídkování → pronásledování) |
| `PlayerMovement.cs` | `RayVisualizer` | Pohyb kliknutím přes NavMesh |
| `PlayerLightDetector.cs` | `AreaLightDetector` | Detekce nepřítele ve světle pomocí OverlapSphere + raycast |
| `EnemyVisibility.cs` | `EnemyVisibility` | Spustí pronásledování při detekci |
| `PlayerDeath.cs` | `PlayerDeath` | Vyřeší souboj při srážce s nepřítelem |
| `DisplayInfo.cs` | `DisplayInfo` | Zobrazí obrazovku výsledků se skóre |
| `Exit.cs` | `Exit` | Spustí útěk při kontaktu s východem |

## Sestavení

Otevři projekt v Unity 2022.3.16f1. Použij **File → Build Settings** pro sestavení. Skripty cílí na C# 9.0 / .NET 4.7.1.
