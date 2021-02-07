# FirstPersonProject
An FPS Unity 3D Project with Climbing mechanics and NavAgents for NPC crowds. This project was developed from scratch between July 2020 - Nov 2020.

## Overview of Features
Movement mechanics (using Unity's Character Controller):
- First Person Perspective Camera
- Standard WASD movement
- Gravity
- Jump, Double Jump
- Sprint
- Climbing

NPC Crowd mechanics (using Unity's Nagivation and Pathfinding):
- NavMesh Agent's ("NPCs") nagivate their way to a random destination object within a certain radius
  - NPCs have a small probability of waiting at destination object, before moving on to new destination

3D Modelling
- Terrain and buildings were generated using open source Geographic Information System data and Blender
- Unity ProBuilder was used to create some buildings
- Some texture materials are free assets made publicly available by Kenney (https://www.kenney.nl/)

## Showcasing Some Features
### First Person Camera
The Main Camera follows an empty GameObject that has been placed at head height on the Character Controller. This means the camera is positioned like a traditional first person perspective camera. Camera rotation of the Y Axis (Left and Right) is applied on the Character Controller, and camera rotation of the X Axis (Up and Down) is applied directly to the Main Camera. These are not applied to the same object to prevent stuttering visual issues. The camera rotation on the X Axis is clamped to prevent it from rotating upside down.
![FPS Camera](https://github.com/bM7tcHF88GBxDni/README-GIF-Storage/blob/main/camera-1.gif)

### Gravity, Jump and Double Jump
![Jumping](https://github.com/bM7tcHF88GBxDni/README-GIF-Storage/blob/main/jumping.gif)

### Climbing
![Climbing](https://github.com/bM7tcHF88GBxDni/README-GIF-Storage/blob/main/climbing.gif)

### NPCs and GIS Modelling
![](https://github.com/bM7tcHF88GBxDni/README-GIF-Storage/blob/main/npcs%20and%20terrain.gif)
