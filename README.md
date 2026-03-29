# ⚔️ Elden Ring Polygon - 3D Action RPG Prototype

A 3D action game prototype built with Unity, inspired by the combat feel and third-person movement style of soulslike games.  
This project focuses on core gameplay systems such as player locomotion, camera follow, weapon handling, combat foundation, and save/load flow.

---

## 📌 Overview

This prototype was created to explore the foundation of a third-person action RPG using Unity.  
The main goal of the project is to build reusable gameplay systems for:

- Third-person character movement
- Camera follow and player input
- Basic melee combat flow
- Weapon equipment and item structure
- Character stats and UI bars
- Save/load menu foundation

---

## ✨ Features

- Third-person player movement and camera system
- Character locomotion with directional movement
- Basic combat and melee weapon framework
- Weapon equipment and inventory structure
- Damage collider setup for melee attacks
- Character stats system (health / stamina foundation)
- Save/load menu structure with save slot UI
- Title screen and world scene flow

---

## 🛠️ Tech Stack

- **Engine:** Unity
- **Language:** C#
- **Pipeline:** URP
- **Input:** Unity Input System
- **UI:** Unity UI / TextMeshPro

---

## 🧱 Project Structure

```text
Scenes/
├── Scene_Main_Menu_01.unity
└── Sence_World_01.unity

Scripts/
├── Characters/
│   ├── CharacterLocomotionManager.cs
│   ├── CharacterCombatManager.cs
│   ├── CharacterEquipmentManager.cs
│   ├── CharacterInventoryManager.cs
│   ├── CharacterStatsManager.cs
│   └── Player/
│       ├── PlayerLocomotionManager.cs
│       ├── PlayerCombatManager.cs
│       ├── PlayerInputManager.cs
│       ├── PlayerEquipmentManager.cs
│       ├── PlayerInventoryManager.cs
│       ├── PlayerStatsManager.cs
│       └── PlayerCamera.cs
├── Colliders/
│   ├── DamageCollider.cs
│   └── MeleeWeaponDamageCollider.cs
├── Game Saving/
│   ├── CharacterSaveData.cs
│   └── SaveFileDataWriter.cs
├── Items/
│   ├── WeaponItem.cs
│   ├── MeleeWeaponItem.cs
│   └── WeaponManager.cs
└── World_Managers/
    ├── WorldSaveGameManager.cs
    └── WorldSoundFXManager.cs
```

## Project Goal
This project focuses on building the core systems of a third-person action RPG, including locomotion, combat setup, weapon handling, and save/load flow.

## Author
Sebastian Graves
