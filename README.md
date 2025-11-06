# 🎮 Florian's 3D Platformer Game

<div align="center">

![Unity](https://img.shields.io/badge/Unity-2022.3.26f1-000000?style=for-the-badge&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Platform](https://img.shields.io/badge/Platform-3D%20Game-0078D4?style=for-the-badge)
![Status](https://img.shields.io/badge/Status-Complete-success?style=for-the-badge)

*A polished 3D platformer game built with Unity, featuring fluid movement mechanics, enemy AI, collectible systems, and multi-level progression.*

[Features](#-features) • [Gameplay](#-gameplay-mechanics) • [Technical Architecture](#-technical-architecture) • [Setup](#-setup-instructions) • [Scripts Documentation](#-comprehensive-scripts-documentation)

</div>

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Features](#-features)
- [Gameplay Mechanics](#-gameplay-mechanics)
- [Technical Architecture](#-technical-architecture)
- [Comprehensive Scripts Documentation](#-comprehensive-scripts-documentation)
- [Project Structure](#-project-structure)
- [Technologies & Tools](#-technologies--tools)
- [Setup Instructions](#-setup-instructions)
- [Game Design Decisions](#-game-design-decisions)
- [Future Enhancements](#-future-enhancements)

---

## 🎯 Overview

**Florian's 3D Platformer** is a fully-featured 3D platformer game demonstrating advanced Unity game development practices. The project showcases a complete game loop with intuitive player controls, dynamic enemy behaviors, collectible mechanics, and seamless level progression. Built with Unity 2022.3.26f1 and Universal Render Pipeline (URP), this game serves as a comprehensive example of modern 3D game development workflows.

The game features a responsive physics-based movement system, intelligent waypoint-based enemy AI, interactive platform mechanics, and a polished audio-visual experience. Each system is modularly designed, making the codebase maintainable and extensible for future enhancements.

---

## ✨ Features

### Core Gameplay Systems
- **Physics-Based Player Movement** - Smooth, responsive character controls with ground detection
- **Combat Mechanics** - Stomp-based enemy elimination system
- **Collectible System** - Coin collection with real-time UI updates
- **Multi-Level Progression** - Seamless scene transitions between levels
- **Death & Respawn System** - Automatic level reloading with visual feedback

### Advanced Mechanics
- **Waypoint-Based Enemy AI** - Dynamic enemy patrol patterns
- **Moving Platform System** - Player synchronization with moving platforms
- **Rotating Objects** - Animated collectibles with configurable rotation axes
- **Audio Integration** - Sound effects for jumps, collections, and death events

### User Interface
- **Start Menu** - Clean entry point with scene navigation
- **End Screen** - Polished game completion interface
- **Real-Time HUD** - Coin counter with live updates

---

## 🎮 Gameplay Mechanics

### Player Controls
- **WASD / Arrow Keys** - Horizontal and vertical movement
- **Space Bar** - Jump (only when grounded)
- **Stomp Attack** - Jump on enemy heads to eliminate them and gain extra jump height

### Game Mechanics
1. **Collect Coins** - Gather collectibles scattered throughout levels to increase your score
2. **Avoid Enemies** - Navigate around or defeat enemies by jumping on their heads
3. **Reach the Goal** - Find and enter the level completion trigger to advance
4. **Survive** - Avoid falling into pits or colliding with enemies

### Death Conditions
- Falling below the world boundary (y < -1)
- Collision with enemy body (not head)

---

## 🏗️ Technical Architecture

### System Design Philosophy
The project follows a **component-based architecture** pattern, where each script encapsulates a specific game mechanic. This modular approach ensures:

- **Separation of Concerns** - Each script handles one primary responsibility
- **Maintainability** - Easy to locate, modify, and extend functionality
- **Reusability** - Components can be easily attached to different GameObjects
- **Testability** - Individual systems can be tested in isolation

### Core Systems Integration
- **Unity Physics Engine** - Rigidbody-based movement and collision detection
- **Input System** - Legacy input manager for cross-platform compatibility
- **Scene Management** - Dynamic scene loading and transitions
- **Audio System** - Event-driven sound effect playback
- **UI System** - Canvas-based interface with real-time updates

---

## 📚 Comprehensive Scripts Documentation

### 🎯 Movement.cs

**Purpose**: Handles all player movement mechanics including walking, running, and jumping with physics-based controls.

#### Class Overview
The `Movement` class is attached to the player GameObject and manages character locomotion through Unity's Rigidbody component. It implements precise ground detection and jump mechanics while maintaining responsive controls.

#### Key Components
- `Rigidbody rb` - Reference to the player's Rigidbody component for physics-based movement
- `movementForce` (float) - Configurable horizontal movement speed multiplier
- `jumpForce` (float) - Configurable vertical jump strength
- `groundCheck` (Transform) - Reference point for ground detection sphere cast
- `ground` (LayerMask) - Layer mask defining what constitutes "ground" for jump validation
- `jumpSound` (AudioSource) - Audio component for jump sound effects

#### Methods

##### `void Start()`
**Initialization Method**
- Retrieves and caches the Rigidbody component reference
- Ensures all physics-based movement operations have proper component access
- Called once when the GameObject is first enabled

##### `void Update()`
**Frame-by-Frame Movement Processing**
- Retrieves horizontal and vertical input axes from Unity's Input Manager
- Applies movement force to the Rigidbody while preserving existing Y-velocity for gravity
- Checks for jump input and validates ground contact before allowing jump
- Executes every frame to ensure responsive, real-time control

**Movement Calculation**:
```csharp
rb.velocity = new Vector3(horizontalinput * movementForce, rb.velocity.y, verticalinput * movementForce);
```
This preserves the Y-axis velocity (gravity) while applying horizontal movement, creating natural physics-based locomotion.

##### `private void Jump()`
**Jump Execution Method**
- Applies upward force to the Rigidbody while maintaining horizontal momentum
- Triggers jump sound effect for audio feedback
- Called internally when jump conditions are met

**Implementation Details**:
- Sets Y-velocity directly to `jumpForce` for consistent jump height
- Preserves X and Z velocities to allow jumping while moving
- Plays audio feedback immediately upon jump initiation

##### `private void OnCollisionEnter(Collision collision)`
**Collision Detection Handler**
- Monitors collisions with "EnemyHead" tagged objects
- Implements stomp mechanic: destroying enemy and triggering bounce jump
- Provides dynamic combat interaction through collision physics

**Stomp Mechanic**:
- Destroys the parent enemy GameObject when head is stomped
- Triggers additional jump to provide satisfying bounce effect
- Creates Mario-style platformer combat feel

##### `bool IsGrounded()`
**Ground Detection System**
- Uses Physics.CheckSphere to detect ground contact
- Sphere cast from `groundCheck` position with 0.1 unit radius
- Filters collisions using `ground` LayerMask for precise detection
- Returns boolean indicating whether player can perform jump

**Technical Implementation**:
```csharp
return Physics.CheckSphere(groundCheck.position, .1f, ground);
```
This method provides frame-perfect ground detection without requiring continuous collision checks, optimizing performance while maintaining accuracy.

---

### 💰 ItemCollector.cs

**Purpose**: Manages collectible item interactions, tracking, and UI updates for the coin collection system.

#### Class Overview
The `ItemCollector` class handles all collectible interactions through Unity's trigger system. It maintains a running coin count and provides real-time UI feedback to enhance player engagement.

#### Key Components
- `coins` (int) - Private counter tracking total collected coins
- `collectionSound` (AudioSource) - Audio feedback for successful collection
- `coinsText` (Text) - UI Text component displaying current coin count

#### Methods

##### `private void OnTriggerEnter(Collider other)`
**Trigger-Based Collection System**
- Monitors collision triggers with objects tagged "Coin"
- Automatically destroys collected coin GameObjects
- Increments coin counter and updates UI display
- Plays collection sound effect for immediate feedback

**Collection Workflow**:
1. Player enters trigger volume of coin
2. Coin GameObject is immediately destroyed (removed from scene)
3. Coin counter increments
4. UI text updates with new count: `"coins: X"`
5. Audio feedback plays

**Design Rationale**: Using triggers instead of collisions allows coins to be collected without physical interaction, creating smoother gameplay. The immediate destruction prevents double-collection bugs.

---

### 💀 PlayerLoif.cs

**Purpose**: Manages player death conditions, visual feedback, and level reset functionality.

#### Class Overview
The `PlayerLoif` class acts as the game's health and death management system. It monitors multiple death conditions, handles visual feedback, and coordinates scene reloading for seamless respawning.

#### Key Components
- `dead` (bool) - Flag preventing multiple death triggers
- `deathSound` (AudioSource) - Audio component for death event feedback

#### Methods

##### `private void Update()`
**Continuous Death Monitoring**
- Checks player Y-position every frame
- Triggers death if player falls below y = -1 (world boundary)
- Prevents multiple death triggers using `dead` flag

**Boundary Detection**: This provides a safety net for players who fall off platforms, automatically resetting the level rather than leaving them in an unrecoverable state.

##### `private void OnCollisionEnter(Collision collision)`
**Collision-Based Death Detection**
- Monitors collisions with objects tagged "Enemy"
- Triggers death sequence when enemy body is touched
- Disables visual rendering for immediate feedback
- Sets Rigidbody to kinematic to prevent physics interactions
- Disables Movement component to prevent post-death control

**Death Sequence**:
1. Death method is called
2. MeshRenderer is disabled (player becomes invisible)
3. Rigidbody becomes kinematic (no physics)
4. Movement component is disabled (no input processing)

##### `private void MarrShaashika()`
**Death Execution Method**
- Sets death flag to prevent duplicate triggers
- Schedules level reload after 1.3 second delay using `Invoke`
- Plays death sound effect
- Logs death event for debugging

**Delayed Reload**: The 1.3 second delay allows players to see/hear death feedback before level reset, improving user experience and preventing jarring instant transitions.

##### `void ReloadLevel()`
**Scene Reset Handler**
- Loads the current active scene by name
- Resets all game state to initial conditions
- Provides seamless respawn experience

**Scene Management**: Uses `SceneManager.LoadScene()` with active scene name to ensure level-specific respawning, maintaining level progression structure.

---

### 🤖 WaypointFollower.cs

**Purpose**: Implements intelligent waypoint-based AI for enemy movement patterns.

#### Class Overview
The `WaypointFollower` class provides a reusable AI system for objects that need to follow predefined paths. It creates smooth, predictable movement patterns ideal for enemy patrols and moving platforms.

#### Key Components
- `wayPoints` (GameObject[]) - Array of waypoint positions defining the patrol path
- `currentWaypointIndex` (int) - Index tracking current destination waypoint
- `speed` (float) - Configurable movement speed in units per second

#### Methods

##### `void Update()`
**Waypoint Navigation Loop**
- Calculates distance to current waypoint target
- Advances to next waypoint when within 0.1 unit threshold
- Loops back to first waypoint when reaching end of array
- Moves GameObject toward current waypoint using frame-rate independent movement

**Navigation Algorithm**:
1. **Distance Check**: Calculates 3D distance between current position and target waypoint
2. **Threshold Detection**: When distance < 0.1 units, considers waypoint reached
3. **Index Advancement**: Increments waypoint index
4. **Loop Logic**: Resets to index 0 when exceeding array bounds
5. **Movement**: Uses `Vector3.MoveTowards` for smooth, consistent movement

**Frame-Rate Independence**:
```csharp
transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);
```
Multiplies speed by `Time.deltaTime` to ensure consistent movement speed regardless of framerate, essential for professional game development.

**Design Benefits**:
- **Reusable**: Can be attached to any GameObject needing waypoint movement
- **Configurable**: Speed and waypoints adjustable in Unity Inspector
- **Smooth**: Frame-rate independent movement ensures consistent behavior
- **Flexible**: Supports paths of any length and complexity

---

### 🚪 NextLevel.cs

**Purpose**: Handles level progression by detecting when player reaches the level completion trigger.

#### Class Overview
The `NextLevel` class provides a simple, elegant solution for level transitions. It uses Unity's trigger system to detect player arrival at level goals and automatically advances to the next scene.

#### Key Components
None - This is a lightweight, single-purpose component.

#### Methods

##### `private void OnTriggerEnter(Collider other)`
**Level Completion Detection**
- Monitors trigger collisions with objects named "Player"
- Loads next scene in build index when player enters trigger
- Uses scene build index for reliable scene ordering

**Scene Progression**:
```csharp
SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
```
This approach ensures scenes are loaded in the order defined in Unity's Build Settings, making level management straightforward and maintainable.

**Design Pattern**: This component follows the "trigger zone" pattern common in platformer games, providing a clear, visual way to mark level completion points.

---

### 🎬 StartMenu.cs

**Purpose**: Handles main menu navigation and game initialization.

#### Class Overview
The `StartMenu` class provides the entry point for the game, managing the transition from the start screen to the first gameplay level.

#### Key Components
None - Pure functionality class with no state variables.

#### Methods

##### `public void StartGame()`
**Game Initialization Method**
- Loads the next scene in build index (first gameplay level)
- Called by UI button click event
- Provides clean entry point into game experience

**Usage**: This method is typically connected to a "Play" or "Start" button in the Unity UI system, allowing players to begin gameplay from the main menu.

---

### 🏁 EndMenu.cs

**Purpose**: Manages game completion screen and application exit functionality.

#### Class Overview
The `EndMenu` class handles the end-of-game experience, providing players with a way to exit the application after completing all levels.

#### Key Components
None - Minimal interface class.

#### Methods

##### `public void SooBaWashaKay()`
**Application Exit Handler**
- Closes the Unity application
- Called by UI button (typically "Quit" or "Exit" button)
- Provides clean application termination

**Platform Compatibility**: `Application.Quit()` works across all Unity-supported platforms, though behavior may vary slightly (e.g., no effect in Editor mode).

---

### 🎚️ PlatStick.cs

**Purpose**: Implements player synchronization with moving platforms using Unity's transform parenting system.

#### Class Overview
The `PlatStick` class ensures players move correctly when standing on moving platforms. It uses Unity's transform hierarchy to maintain player position relative to platform movement.

#### Key Components
None - Event-driven component with no persistent state.

#### Methods

##### `private void OnCollisionEnter(Collision collision)`
**Platform Attachment Handler**
- Detects when player collides with platform
- Parents player transform to platform transform
- Ensures player moves with platform automatically

**Transform Parenting**: By setting the player as a child of the platform, Unity automatically handles all position calculations relative to the platform's movement. This is more efficient and reliable than manually calculating offsets.

##### `private void OnCollisionExit(Collision collision)`
**Platform Detachment Handler**
- Detects when player leaves platform collision
- Unparents player transform (sets to null)
- Returns player to world-space movement

**Synchronization Flow**:
1. Player lands on platform → `OnCollisionEnter` → Player becomes child of platform
2. Player moves with platform automatically via transform hierarchy
3. Player jumps off platform → `OnCollisionExit` → Player becomes independent
4. Player movement returns to normal world-space controls

**Technical Benefits**:
- **Automatic**: No manual position calculations required
- **Accurate**: Unity handles all frame-rate and physics considerations
- **Efficient**: Leverages Unity's optimized transform system
- **Robust**: Handles edge cases like platform rotation automatically

---

### 🔄 Rotation.cs

**Purpose**: Provides configurable rotation animation for GameObjects, primarily used for collectible coins.

#### Class Overview
The `Rotation` class creates smooth, continuous rotation animations along any combination of axes. It's designed to make collectibles visually appealing and draw player attention.

#### Key Components
- `speedx` (float) - Rotation speed multiplier for X-axis (roll)
- `speedy` (float) - Rotation speed multiplier for Y-axis (pitch)
- `speedz` (float) - Rotation speed multiplier for Z-axis (yaw)

#### Methods

##### `void Update()`
**Continuous Rotation Animation**
- Applies rotation every frame using `Transform.Rotate()`
- Multiplies by 360 degrees to convert speed multiplier to full rotations
- Uses `Time.deltaTime` for frame-rate independent animation
- Configurable per-axis for flexible rotation patterns

**Rotation Calculation**:
```csharp
transform.Rotate(360 * speedx * Time.deltaTime, 360 * speedy * Time.deltaTime, 360 * speedz * Time.deltaTime);
```

**Design Flexibility**:
- **X-axis (speedx)**: Creates rolling motion
- **Y-axis (speedy)**: Creates spinning motion (most common for coins)
- **Z-axis (speedz)**: Creates tumbling motion
- **Combinations**: Mix axes for complex rotation patterns

**Example Configurations**:
- Coin: `speedx=0, speedy=2, speedz=0` (spins on vertical axis)
- Power-up: `speedx=1, speedy=1, speedz=1` (tumbles in all directions)
- Platform decoration: `speedx=0.5, speedy=0, speedz=0` (slow roll)

---

## 📁 Project Structure

```
Florian-s-3D-game/
│
├── Assets/
│   ├── CasualGameBGM05/          # Audio assets (BGM and SFX)
│   ├── Materials/                 # Visual materials (Coin, Enemy, LevelBarrier)
│   ├── Physics material/          # Custom physics material
│   ├── prefabs/                   # Reusable game objects
│   │   ├── Coin.prefab
│   │   ├── Enemy.prefab
│   │   ├── floor.prefab
│   │   └── levelBarrier.prefab
│   ├── Scenes/                    # Game levels and menus
│   │   ├── startScreen.unity
│   │   ├── 01.unity
│   │   ├── 02.unity
│   │   └── endScreen.unity
│   └── scripts/                   # C# game logic scripts
│       ├── Movement.cs
│       ├── ItemCollector.cs
│       ├── PlayerLoif.cs
│       ├── WaypointFollower.cs
│       ├── NextLevel.cs
│       ├── StartMenu.cs
│       ├── EndMenu.cs
│       ├── PlatStick.cs
│       └── Rotation.cs
│
├── Packages/                       # Unity package dependencies
│   ├── manifest.json
│   └── packages-lock.json
│
├── ProjectSettings/                # Unity project configuration
│   ├── ProjectVersion.txt
│   ├── EditorBuildSettings.asset
│   └── [other Unity settings]
│
└── UserSettings/                   # Editor-specific settings
```

---

## 🛠️ Technologies & Tools

### Core Technologies
- **Unity Engine** - 2022.3.26f1 (LTS)
- **C#** - Primary programming language
- **Universal Render Pipeline (URP)** - Modern rendering pipeline

### Unity Systems Utilized
- **Physics System** - Rigidbody, Colliders, Triggers
- **Input System** - Legacy Input Manager
- **Scene Management** - Dynamic scene loading
- **Audio System** - AudioSource components
- **UI System** - Canvas and Text components
- **Transform System** - Parenting for platform mechanics

### Development Tools
- **Unity Editor** - Primary development environment
- **Visual Studio / VS Code** - Code editing and debugging
- **Unity Package Manager** - Dependency management

---

## 🚀 Setup Instructions

### Prerequisites
- **Unity Hub** installed on your system
- **Unity Editor** version 2022.3.26f1 or compatible LTS version
- **Git** (optional, for version control)

### Installation Steps

1. **Clone or Download the Repository**
   ```bash
   git clone [repository-url]
   cd Florian-s-3D-game
   ```

2. **Open in Unity**
   - Launch Unity Hub
   - Click "Add" and select the project folder
   - Ensure Unity 2022.3.26f1 is installed
   - Open the project

3. **Verify Project Setup**
   - Unity will automatically import all assets
   - Wait for asset import to complete
   - Check Console for any import errors

4. **Configure Build Settings**
   - Go to `File > Build Settings`
   - Verify scenes are in correct order:
     1. startScreen
     2. 01
     3. 02
     4. endScreen

5. **Test the Game**
   - Press Play in Unity Editor
   - Use WASD/Arrow Keys to move
   - Use Space to jump
   - Collect coins and reach the goal!

### Build Instructions

1. **Select Target Platform**
   - Go to `File > Build Settings`
   - Choose your target platform (Windows, Mac, Linux, etc.)

2. **Configure Player Settings** (Optional)
   - Click "Player Settings"
   - Configure resolution, icon, etc.

3. **Build**
   - Click "Build" or "Build and Run"
   - Choose output directory
   - Wait for build to complete

---

## 🎨 Game Design Decisions

### Movement System
**Decision**: Physics-based movement using Rigidbody velocity manipulation.

**Rationale**: 
- Provides natural, responsive feel
- Integrates seamlessly with Unity's physics
- Allows for easy tweaking of movement parameters
- Supports complex interactions (stomping, bouncing)

### Ground Detection
**Decision**: Sphere cast from dedicated ground check point.

**Rationale**:
- More accurate than simple collision checks
- Prevents false positives from walls
- Configurable detection radius
- Performance-efficient single check per frame

### Death System
**Decision**: Delayed respawn (1.3 seconds) with visual/audio feedback.

**Rationale**:
- Allows players to process death event
- Provides satisfying feedback
- Prevents jarring instant resets
- Maintains game flow

### Waypoint AI
**Decision**: Array-based waypoint system with distance thresholding.

**Rationale**:
- Simple to implement and understand
- Highly configurable in Inspector
- Supports paths of any complexity
- Frame-rate independent movement

### Platform Sticking
**Decision**: Transform parenting instead of manual position tracking.

**Rationale**:
- Leverages Unity's optimized transform system
- Handles rotation automatically
- No manual calculations needed
- More reliable than custom solutions

---

## 🔮 Future Enhancements

### Potential Improvements
- [ ] **New Input System** - Migrate to Unity's modern Input System for better cross-platform support
- [ ] **Coin Persistence** - Save coin count across levels or game sessions
- [ ] **Pause Menu** - Add pause functionality with options
- [ ] **Level Select** - Allow players to choose levels after completion
- [ ] **Score System** - Implement time-based scoring and leaderboards
- [ ] **Particle Effects** - Add visual feedback for coin collection and enemy defeat
- [ ] **Checkpoint System** - Implement respawn points throughout levels
- [ ] **Sound Manager** - Centralized audio management system
- [ ] **Settings Menu** - Volume controls, graphics options
- [ ] **Mobile Support** - Touch controls and mobile optimizations

### Code Quality Improvements
- [ ] **Naming Convention** - Fix typo: `PlayerLoif` → `PlayerLife`
- [ ] **Code Comments** - Add comprehensive English comments
- [ ] **ScriptableObjects** - Use for game configuration data
- [ ] **Event System** - Implement Unity Events for decoupled communication
- [ ] **Singleton Pattern** - Game manager for global state management

---

## 📝 License

This project is available for portfolio demonstration purposes. Audio assets are from Casual Game BGM Pack #5 (free use, no resale).

---

## 👤 Author

**Florian** - Game Developer & Unity Enthusiast

*This project demonstrates proficiency in Unity 3D game development, C# programming, game design principles, and software architecture.*

---

<div align="center">

**Built with ❤️ using Unity**

*Last Updated: 2024*

</div>

