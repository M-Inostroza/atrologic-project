# Atrologic Project

This repository contains the source for the **Atrologic** Unity project. The
project was created with Unity `6000.0.28f1`. Opening it with other versions
may cause unexpected issues, so it is recommended to use the same editor
version when possible.

## Setup

1. Install Unity `6000.0.28f1` via Unity Hub.
2. Clone this repository and open the project folder from Unity Hub or the
   Unity editor. Unity will import all required packages automatically on first
   load.

## Building the game

1. Open the project in Unity.
2. Choose **File → Build Settings...**.
3. Ensure the desired platform is selected (e.g. `PC, Mac & Linux` or another
   target). The build settings already include the scenes below.
4. Click **Build** and select an output directory.

## Scenes

The build configuration lists the main scenes located in `Assets/Scenes/`:

- **Start Menu** – entry scene containing the game's menu.
- **Base** – the hub or base camp scene used when the player is not in a level.
- **Level** – the gameplay scene where levels are loaded.

You can open these scenes directly from the Unity editor for testing or modify
the build order in `ProjectSettings/EditorBuildSettings.asset` if necessary.

## License

This project is licensed under the terms of the [MIT License](LICENSE).
