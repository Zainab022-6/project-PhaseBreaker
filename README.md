# Phase-Shifter

**Phase-Shifter** is a first-person puzzle platformer built around a single core idea:  
**platforms are unstable by default and only exist when correctly phased.**

The game removes combat entirely and focuses on spatial reasoning, timing, and player intent. Every jump is a decision, and every mistake has immediate consequences.

---

## 🎮 Core Gameplay Mechanics

- All platforms begin in a **non-solid (ghosted)** state.
- Stepping onto a ghosted platform causes the player to fall.
- The player is equipped with a **Phase Gun** that temporarily stabilizes platforms:
  - **Left Mouse Button** → Phases **Blue** platforms
  - **Right Mouse Button** → Phases **Red** platforms
- Platforms only become solid when phased with the **correct color**.
- Using the **wrong color** will either do nothing or destabilize an already solid platform.

The mechanic is intentionally simple but designed to scale in complexity through level design and timing constraints.

---

## 🧩 Level Design & Progression

### **Prologue (Level 1)**
- Serves as a tutorial level.
- Introduces:
  - Ghosted platforms
  - Phase Gun usage
  - Color-based interaction
- Platforms remain solid once phased.
- No checkpoints.
- Falling respawns the player at the starting platform.

---

### **Level 2**
- Builds on the core mechanic by introducing **temporary stability**.
- Platforms revert back to a ghosted state after a short duration.
- Encourages continuous movement and route planning.
- No checkpoints.
- Falling respawns the player at the starting platform.

---

### **Level 3 (Final Level)**
- Introduces a **Checkpoint System**:
  - Falling **before** the checkpoint respawns the player at the start.
  - Falling **after** the checkpoint respawns the player at the checkpoint.
- Designed as a final test of:
  - Timing
  - Color accuracy
  - Movement planning
- Combines all previously introduced mechanics without additional tutorials.

---

## 🛠️ Technical Overview

- **Engine:** Unity (Windows build)
- **Language:** C#
- **Architecture:** Scene-based level flow
- **UI:** Unity UI + TextMeshPro
- **Input:** Keyboard & Mouse
- **Physics:** CharacterController-based movement

---

## 👥 Team & Contributions

This project was developed collaboratively as part of a hackathon.

### **Mohammad Hamza Qureshi**
- Designed and implemented all **core gameplay systems**
- Phase Gun mechanics
- Platform phasing logic
- Respawn and checkpoint systems
- Scene transitions and level flow
- Gameplay scripting and system integration

### **Zainab Sheikh**
- Level design and environment layout
- Platform placement and progression pacing
- Gameplay flow through spatial design

### **Afrida Ali**
- UI Menu design
- Main menu navigation
- Pause menu system (ESC key)
- UI buttons and user flow

---

## 🎨 Assets & Tools Used

Third-party assets used in this project:

- **Free Lowpoly Sci-Fi Objects** — Black Rose Developers  
- **Grid Master** — Black Rose Developers  
- **RETROWAVE SKIES Lite** — Suggo Creations  
- **Cyber Box** — Tausoft  
- **Mini First Person Controller** — Simon Serge Pasi  
- **Decimate** — Unity Asset Store tool  

All assets are used under their respective licenses.

---

## 📦 Build & Execution

- Platform: **Windows (64-bit)**
- Extract the build ZIP completely.
- Run `Phase-Shifter.exe`.
- Do not remove or modify the `_Data` folder.

---

## 🧠 Design Intent

Phase-Shifter is designed to reward **understanding over speed**.  
The player is never rushed by enemies or combat, but by the consequences of their own decisions.

The game aims to create tension through instability — where standing still is often more dangerous than moving forward.

---

## 📄 License

This project is licensed under the **MIT License**.  
See the `LICENSE` file for details.
