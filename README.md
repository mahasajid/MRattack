# Clickjacking in Collaborative Mixed Reality (HoloLens 2)

**Research prototype | Unity / HoloLens 2 | Branch: `clickjack`**

This branch explores **interaction redirection (clickjacking) in a collaborative mixed-reality environment**. The prototype investigates a mismatch between the virtual object a user appears to manipulate and the object that actually receives the manipulation. It is intended for controlled research into interaction integrity and trust in shared MR applications—not as a claim of an operating-system-level HoloLens 2 vulnerability.

## Research question

In shared MR, users rely on visible holograms, hand/pointer input, and synchronized objects to understand the effects of their actions. What happens when the apparent interaction target and the object whose position is updated are different?

The example demonstrates how application-side object handlers can redirect a drag from one shared virtual object to another, creating a discrepancy between **user intent** and **resulting state changes**.


The effect being investigated is **object/action misbinding**, rather than exploitation of the HoloLens operating system. Whether changes propagate to another device depends on the application's networking and synchronization configuration.

## Repository guide

| Path | Role |
| --- | --- |
| [`SS_CJ/`](SS_CJ/) | Unity project for this branch |
| [`Assets/Scenes/MainCJ.unity`](SS_CJ/Assets/Scenes/MainCJ.unity) | Clickjacking demonstration scene; contains the `ClickJack` controller |
| [`ClickJacking.cs`](SS_CJ/Assets/ScriptTemplates/Scripts/ClickJacking.cs) | Finds target objects and installs redirection components |
| [`ProxyObjectManipulator.cs`](SS_CJ/Assets/ScriptTemplates/Scripts/ProxyObjectManipulator.cs) | Handles pointer-down/drag/up and moves the paired target |
| [`Assets/Prefabs/`](SS_CJ/Assets/Prefabs/) | Shared-object and player prefabs |
| [`Assets/ScriptTemplates/Scripts/`](SS_CJ/Assets/ScriptTemplates/Scripts/) | Supporting collaboration, spatial alignment, mesh, and video scripts |

Additional occlusion/spatial-interference experiments are present in the project, but **`MainCJ.unity` and the two clickjacking scripts above** are the starting point for this branch's central demonstration.

## Requirements

- **Microsoft HoloLens 2** for headset-based testing
- **Unity 2019.4.38f1** (version recorded in `SS_CJ/ProjectSettings/ProjectVersion.txt`)
- **Microsoft Mixed Reality Toolkit (MRTK)**; the project manifest references packages from **2.7.0** and **2.8.3**
- **Mirror** networking (included in project assets)
- **MixedReality-WebRTC 2.0.2** (referenced by the package manifest)
- A compatible Windows / Unity build environment for HoloLens 2 deployment, if testing on-device

The repository also includes OpenCV-related components and local package archives. Because this is a research snapshot, some dependencies and device/network configuration may require adjustment in a current environment.

## Open and inspect the demo

1. Clone the relevant branch:
   ```bash
   git clone --branch clickjack https://github.com/mahasajid/MRattack.git
   ```
2. In Unity Hub, add **`SS_CJ/`** as the Unity project (not the repository root).
3. Open **`Assets/Scenes/MainCJ.unity`**.
4. Inspect the **`ClickJack`** GameObject and its `ClickJacking` component.
5. For an end-to-end test, configure the project's existing collaborative/networking setup and create the shared remote objects expected by the script. The redirection logic will not activate with fewer than six `ObjectFinder` objects.


For the related spatial-interference prototype, see the [`spatial` branch](https://github.com/mahasajid/MRattack/tree/spatial).
