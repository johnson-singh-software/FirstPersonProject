# FirstPersonProject
FPS Unity 3D Project with Climbing mechanics and NavMesh Agents for NPC crowds. This project was developed from scratch between June 2020 - Nov 2020.

## Overview of Features
Movement mechanics (using Unity's Character Controller):
- First Person Perspective Camera
- Standard WASD movement
- Gravity
- Jump, Double Jump
- Sprint
- Climbing

NPC Crowd mechanics (using Unity's Navigation and Pathfinding):
- NavMesh Agent's ("NPCs") navigate their way to a random destination object within a certain radius
  - NPCs have a small probability of waiting at destination object, before moving on to new destination

3D Modelling
- Terrain and buildings were generated using open source Geographic Information System data and Blender
- Unity ProBuilder was used to create some buildings
- Some texture materials are free assets made publicly available by Kenney (https://www.kenney.nl/)

## Showcasing Some Features

### Climbing
A ray is cast in the direction the player is facing at a very close distance. If the object returned is “scalable” (setup as a layer mask property in Unity) I use the normal of that ray’s hit point to calculate the perpendicular directions that the player can move when climbing. I need to use the normal so the climbing movement is independent of the angle the player is facing the object. 
![](https://github.com/bM7tcHF88GBxDni/README-GIF-Storage/blob/main/sideview.png)
![](https://github.com/bM7tcHF88GBxDni/README-GIF-Storage/blob/main/topview.png)
After contact with a scalable object, movement switches from Z Axis (Forward and Backward) to “scaling” mode (an enum Player.status flag) where Vertical Input is now along the Y Axis (Up and Down). If the player climbs down to the floor, the grounding check will switch movement back to default/walking.

Initially, the player was able to climb and look simultaneously. For design purposes I have implemented a rotation check so the player cannot climb while facing completely away from the object. This gives the climbing mechanic some weight and a purposeful feeling. It also prevents disorientation: if a player is moving in one direction, turns to look behind them, lets go of the movement direction key and presses the same direction key again, the character will move in the opposite direction. This is not a bug but can be confusing for the player.

![](https://github.com/bM7tcHF88GBxDni/README-GIF-Storage/blob/main/topviewclimb.png)

Final climbing example:

![Climbing](https://github.com/bM7tcHF88GBxDni/README-GIF-Storage/blob/main/climbing.gif)

### First Person Camera
The Main Camera follows an empty GameObject that has been placed at head height on the Character Controller. This means the camera is positioned like a traditional first person perspective camera. Camera rotation of the Y Axis (Left and Right) is applied on the Character Controller, and camera rotation of the X Axis (Up and Down) is applied directly to the Main Camera. These are not applied to the same object to prevent stuttering visual issues. The camera rotation on the X Axis is clamped to prevent it from rotating upside down.
![FPS Camera](https://github.com/bM7tcHF88GBxDni/README-GIF-Storage/blob/main/camera-1.gif)

### Gravity, Jump and Double Jump
As Character Controllers are not a part of Unity’s Physics engine they are not affected by Rigidbody, forces and gravity. I created a gravity method which applies a negative Y Axis transform to the Character Controller at all times. When jumping, the “jumping force” (a positive transform on the Y axis) needs to overcome this gravity transform. Keeping gravity enabled when jumping allows for a natural acceleration and deacceleration lerp. An Overlap Boxcast also checks the player is grounded before being able to jump. I then implemented a double jump by running a check against a double jump flag which is enabled after a single jump and disabled again after the double jump.
![Jumping](https://github.com/bM7tcHF88GBxDni/README-GIF-Storage/blob/main/jumping.gif)

### NPCs and GIS Modelling
NPCs navigate between nodes placed around the GIS map. They will randomly travel between destination nodes within a certain radius. All non vertical surfaces on this map can be traversed using Unity's built in Pathfinding systems. I explored this to create a sense of place in this otherwise empty location. 

![](https://github.com/bM7tcHF88GBxDni/README-GIF-Storage/blob/main/npcs%20and%20terrain.gif)
