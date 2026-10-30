# Spatial Interference / Occlusion in Collaborative Mixed Reality (HoloLens 2)

**Research prototype | Unity / HoloLens 2 | Branch: `spatial`**

This branch investigates **spatial interference with shared virtual objects in mixed reality**, focusing on how application-controlled geometry can be placed at or around remotely represented objects. The code includes occlusion-cube positioning, renderer-visibility experiments, and an experimental transparent-collider approach. It is a controlled Unity/HoloLens 2 research prototype, **not a demonstrated HoloLens OS exploit**.


## How the prototype works

Two scene-attached controllers are the main starting point:

- [`AttackScript.cs`](SS_Spatial/Assets/AttackScript.cs) looks for objects tagged `ObjectFinder`. Once at least three are present, it chooses one, collects bounds from its child renderers (with padding), and **continuously updates** an assigned `OcclusionCube` position and scale to follow the selected object.
- [`AttackScript_Spatial.cs`](SS_Spatial/Assets/AttackScript_Spatial.cs) waits until at least four tagged objects are present, picks one, and **positions/scales** the assigned `OcclusionCube` from its bounds **once**, when a target is found.

Both scripts are referenced by [`Assets/Scenes/Main.unity`](SS_Spatial/Assets/Scenes/Main.unity). They reference the same named occlusion cube in the scene; if both controllers remain active, their writes can overlap. Inspect which controller is enabled for the particular experimental condition.



## Repository guide

| Path | Role |
| --- | --- |
| [`SS_Spatial/`](SS_Spatial/) | Unity project for this branch |
| [`Assets/Scenes/Main.unity`](SS_Spatial/Assets/Scenes/Main.unity) | Main shared-MR scene with spatial attack controllers |
| [`AttackScript.cs`](SS_Spatial/Assets/AttackScript.cs) | Continuously follows the bounds of a selected shared object |
| [`AttackScript_Spatial.cs`](SS_Spatial/Assets/AttackScript_Spatial.cs) | One-time bounds-based occlusion-cube placement |
| [`FindRemoteObject.cs`](SS_Spatial/Assets/FindRemoteObject.cs) | Experimental transparent geometry/collider creation |
| [`Assets/Prefabs/`](SS_Spatial/Assets/Prefabs/) | Player and shared-object prefabs |
| [`Assets/ScriptTemplates/Scripts/`](SS_Spatial/Assets/ScriptTemplates/Scripts/) | Networking, portal alignment, video, and mesh processing code |

## Requirements

- **Microsoft HoloLens 2** for headset-based evaluation
- **Unity 2019.4.38f1** (see `SS_Spatial/ProjectSettings/ProjectVersion.txt`)
- **Microsoft Mixed Reality Toolkit (MRTK)**; packages from **2.7.0** and **2.8.3** appear in the manifest
- **Mirror** networking (included in project assets)
- **MixedReality-WebRTC 2.0.2** (referenced by the project manifest)
- A compatible Windows / Unity build environment for HoloLens 2 deployment, if testing on-device

The project also contains OpenCV-related code, local Unity package archives, and supporting shared-surface experiments. Current Unity installations or device toolchains may need compatibility adjustments.

## Open and inspect the demo

1. Clone the branch:
   ```bash
   git clone --branch spatial https://github.com/mahasajid/MRattack.git
   ```
2. In Unity Hub, add the **`SS_Spatial/`** folder as the project.
3. Open **`Assets/Scenes/Main.unity`**.
4. Inspect **`AttackController`**, **`AttackController_Spatial`**, and **`OcclusionCube`**. Verify the assigned cube references and enabled components.
5. Use the project's shared-object/network setup to populate `ObjectFinder` objects. The attack scripts require existing remote objects; they do not generate a complete collaborative session on their own.

