# 🎮 Coin Quest

<div align="center">

![Unity](https://img.shields.io/badge/Unity-2022.3.26f1-000000?style=for-the-badge&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Platform](https://img.shields.io/badge/Platform-3D%20Game-0078D4?style=for-the-badge)
![Status](https://img.shields.io/badge/Status-Complete-success?style=for-the-badge)

*Hey there! Welcome to my 3D platformer game - Coin Quest! I built this using Unity, and it features smooth movement mechanics, enemy AI, collectible systems, and multiple levels. Come check it out!*

[Features](#-what-i-built) • [How to Play](#-how-to-play) • [My Code Architecture](#-how-i-structured-everything) • [Getting Started](#-lets-get-you-started) • [Scripts Deep Dive](#-diving-into-my-scripts)

</div>

---

## 📋 What's Inside?

- [What I Built](#-what-i-built)
- [How to Play](#-how-to-play)
- [How I Structured Everything](#-how-i-structured-everything)
- [Diving Into My Scripts](#-diving-into-my-scripts)
- [Project Structure](#-how-i-organized-my-files)
- [Technologies I Used](#-technologies-i-used)
- [Let's Get You Started](#-lets-get-you-started)
- [Why I Made These Design Choices](#-why-i-made-these-design-choices)
- [What I Want to Add Next](#-what-i-want-to-add-next)

---

## 🎯 What I Built

Hey! So I wanted to create a complete 3D platformer game, and this is what I came up with - **Coin Quest**! 

I built this using Unity 2022.3.26f1 with the Universal Render Pipeline (URP), and honestly, I'm pretty proud of how it turned out. The game has everything you'd expect from a platformer: smooth player movement, enemies that actually move around, collectible coins, multiple levels, and a complete game loop from start to finish.

What I really focused on was making sure everything felt responsive and polished. I spent a lot of time tweaking the movement system to make it feel just right, and I made sure each script does one specific job really well. This way, if I want to change something later, I know exactly where to look!

---

## ✨ What Makes It Special

### Core Gameplay Features
- **Physics-Based Movement** - I used Unity's Rigidbody system to make the movement feel natural and responsive. You'll notice the character responds smoothly to your input!
- **Combat System** - Want to defeat enemies? Just jump on their heads! It's that classic Mario-style stomp mechanic that I've always loved.
- **Coin Collection** - Coins are scattered throughout the levels, and I made sure the UI updates in real-time so you always know how many you've collected.
- **Multiple Levels** - I created a seamless progression system that takes you from level to level without any jarring transitions.
- **Death & Respawn** - If you fall or get hit, don't worry! The game automatically reloads the level after a short delay so you can try again.

### Cool Technical Features
- **Smart Enemy AI** - The enemies follow waypoint paths that I set up. They patrol back and forth, making the levels feel more alive!
- **Moving Platforms** - I implemented a system where you stick to moving platforms automatically. No more sliding off!
- **Animated Collectibles** - The coins spin and rotate to catch your eye. I made this configurable so I can easily change how they look.
- **Sound Effects** - I added audio feedback for jumping, collecting coins, and even when you die. It makes everything feel more satisfying!

### User Interface
- **Start Menu** - A clean menu screen to begin your adventure
- **End Screen** - A polished completion screen when you finish all levels
- **Live HUD** - A coin counter that updates instantly as you collect coins

---

## 🎮 How to Play

### Controls
- **WASD or Arrow Keys** - Move your character around
- **Space Bar** - Jump (but only when you're on the ground - I made sure you can't spam jump!)
- **Stomp Attack** - Jump on enemy heads to defeat them and get a nice bounce effect

### What You Need to Do
1. **Collect Coins** - Gather all the shiny coins scattered around each level
2. **Avoid or Defeat Enemies** - Either jump over them or stomp on their heads to eliminate them
3. **Reach the Goal** - Find the level completion trigger to advance to the next level
4. **Don't Die!** - Watch out for falling into pits or touching enemy bodies

### How You Can Die
- Fall below the world boundary (y < -1) - I set this up as a safety net
- Touch an enemy's body (not the head - heads are safe to jump on!)

---

## 🏗️ How I Structured Everything

### My Design Philosophy

When I started building this, I decided to use a **component-based architecture**. What does that mean? Well, each script I wrote handles one specific thing. For example:
- `Movement.cs` only handles player movement
- `ItemCollector.cs` only handles coin collection
- `PlayerLoif.cs` only handles death and respawning

Why did I do this? Because it makes everything so much easier to work with! If I want to change how jumping works, I know exactly where to look. If I want to add a new collectible type, I can modify just the collection script. It's all about keeping things organized and maintainable.

### Systems I Integrated

I used several Unity systems to make everything work together:
- **Unity Physics Engine** - For realistic movement and collisions
- **Input System** - I used the legacy input manager (it's simple and works everywhere)
- **Scene Management** - To handle level transitions smoothly
- **Audio System** - For all those satisfying sound effects
- **UI System** - To display the coin counter and menus

---

## 📚 Diving Into My Scripts

Alright, let me walk you through each script I wrote and explain what I was thinking when I created them!

### 🎯 Movement.cs

**What it does**: This handles all the player movement - walking, running, and jumping.

This was one of the first scripts I wrote, and honestly, it took me a while to get it feeling just right! The script is attached to the player GameObject and uses Unity's Rigidbody component to move the character around.

**Key Variables I Use**:
- `Rigidbody rb` - This is how I interact with Unity's physics system
- `movementForce` - How fast the player moves horizontally (you can tweak this in the Inspector!)
- `jumpForce` - How high the player jumps
- `groundCheck` - A Transform point I use to check if the player is on the ground
- `ground` - A LayerMask that defines what counts as "ground"
- `jumpSound` - The audio that plays when you jump

**How It Works**:

In `Update()`, I check for input every frame and apply movement:
```csharp
rb.velocity = new Vector3(horizontalinput * movementForce, rb.velocity.y, verticalinput * movementForce);
```

Notice how I preserve `rb.velocity.y`? That's because I want gravity to work naturally! If I didn't do this, the player would just float.

For jumping, I created an `IsGrounded()` method that uses `Physics.CheckSphere` to detect if there's ground beneath the player. This prevents you from jumping in mid-air, which would feel weird.

One of my favorite features is the stomp mechanic! When you jump on an enemy's head, the `OnCollisionEnter` method detects it, destroys the enemy, and gives you an extra bounce. It feels so satisfying!

---

### 💰 ItemCollector.cs

**What it does**: Handles collecting coins and updating the UI.

This script is pretty straightforward, but I think it's elegant! When the player touches a coin (using Unity's trigger system), it:
1. Destroys the coin immediately (so you can't collect it twice)
2. Increments the coin counter
3. Updates the UI text to show the new count
4. Plays a collection sound

**Why I Used Triggers**: I used triggers instead of regular collisions because I wanted the coins to be collected just by touching them, without any physical interaction. It makes the gameplay smoother!

**Key Variables**:
- `coins` - My private counter that tracks how many coins you've collected
- `collectionSound` - The satisfying sound that plays when you collect a coin
- `coinsText` - The UI Text component that displays "coins: X"

---

### 💀 PlayerLoif.cs

**What it does**: Manages when the player dies and handles respawning.

Okay, so I know there's a typo in the name (`PlayerLoif` instead of `PlayerLife`), but I'm keeping it for now! This script monitors two death conditions:
1. Falling below y = -1 (the world boundary)
2. Touching an enemy's body

When you die, I wanted to make sure you get proper feedback before the level resets. So I:
1. Make the player invisible (disable the MeshRenderer)
2. Stop all physics and movement
3. Play a death sound
4. Wait 1.3 seconds (using `Invoke`)
5. Reload the level

That 1.3 second delay was important to me - I didn't want the level to reset instantly because that would feel jarring. This way, you can see and hear that you died before everything resets.

**Key Variables**:
- `dead` - A flag that prevents multiple death triggers (trust me, you don't want to die multiple times at once!)
- `deathSound` - The sound effect that plays when you die

---

### 🤖 WaypointFollower.cs

**What it does**: Makes enemies (or any object) follow a path of waypoints.

This is one of my favorite scripts because it's so reusable! I can attach it to any GameObject and give it an array of waypoints, and it will smoothly move between them.

**How It Works**:

Every frame, the script:
1. Checks the distance to the current waypoint
2. If it's close enough (within 0.1 units), it moves to the next waypoint
3. When it reaches the last waypoint, it loops back to the first one

I used `Vector3.MoveTowards` with `Time.deltaTime` to make sure the movement speed is consistent regardless of framerate. This is super important for professional game development!

**Key Variables**:
- `wayPoints` - An array of GameObjects that define the path
- `currentWaypointIndex` - Which waypoint we're currently heading toward
- `speed` - How fast the object moves (configurable in the Inspector)

**Why I Love This Script**: It's so flexible! I can use it for enemies, moving platforms, or even decorative objects. Just drag and drop waypoints in the scene, assign them in the Inspector, and boom - instant path following!

---

### 🚪 NextLevel.cs

**What it does**: Detects when you reach the end of a level and loads the next one.

This script is beautifully simple! It's attached to a trigger zone at the end of each level. When you (the player) enter that trigger, it automatically loads the next scene.

**The Magic Line**:
```csharp
SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
```

This loads the next scene in the build order. I made sure to set up my scenes in the right order in Unity's Build Settings, so this just works!

---

### 🎬 StartMenu.cs

**What it does**: Handles the main menu and starts the game.

This is a super simple script with just one public method: `StartGame()`. When you click the "Play" button on the start screen, it calls this method, which loads the first gameplay level.

I kept it minimal because menus don't need to be complicated - they just need to work!

---

### 🏁 EndMenu.cs

**What it does**: Handles the end screen and lets you quit the game.

Similar to the start menu, this is simple and focused. The method `SooBaWashaKay()` (which is just a fun name I gave it) quits the application when you click the quit button.

---

### 🎚️ PlatStick.cs

**What it does**: Makes the player stick to moving platforms.

This was a fun challenge! When you stand on a moving platform, you need to move with it. I could have manually calculated positions, but Unity has a better way: transform parenting!

**How It Works**:
- When you collide with a platform, I make the player a child of the platform
- Unity automatically handles all the position calculations
- When you jump off, I unparent the player

This is so much easier than trying to manually track platform movement! Unity does all the heavy lifting for me.

**Why This Approach**: It's automatic, accurate, and handles edge cases like platform rotation without me having to write extra code. Sometimes the simplest solution is the best one!

---

### 🔄 Rotation.cs

**What it does**: Makes objects rotate continuously (I use it for the coins).

This script is super flexible! I can configure rotation speed for each axis (X, Y, Z) independently. For the coins, I set it to spin on the Y-axis, which makes them look like they're rotating in place.

**The Formula**:
```csharp
transform.Rotate(360 * speedx * Time.deltaTime, 360 * speedy * Time.deltaTime, 360 * speedz * Time.deltaTime);
```

I multiply by 360 to convert the speed multiplier into full rotations per second. And I use `Time.deltaTime` to make sure it's frame-rate independent.

**Why I Made It Configurable**: I wanted to be able to use this script for different objects. Maybe I'll want a power-up that tumbles in all directions, or a decoration that slowly rolls. With this script, I can do all of that!

---

## 📁 How I Organized My Files

Here's how I structured my project. I tried to keep everything organized so I can find things quickly:

```
Coin-Quest-A-3D-Platformer-ProtoType/
│
├── Assets/
│   ├── CasualGameBGM05/          # All my audio files (music and sound effects)
│   ├── Materials/                 # Visual materials for coins, enemies, etc.
│   ├── Physics material/          # Custom physics material I created
│   ├── prefabs/                   # Reusable game objects
│   │   ├── Coin.prefab           # The coin prefab I use everywhere
│   │   ├── Enemy.prefab          # Enemy prefab with waypoint follower
│   │   ├── floor.prefab          # Floor tiles
│   │   └── levelBarrier.prefab   # Barriers between level sections
│   ├── Scenes/                    # All my game levels and menus
│   │   ├── startScreen.unity     # The main menu
│   │   ├── 01.unity              # First level
│   │   ├── 02.unity              # Second level
│   │   └── endScreen.unity       # Completion screen
│   └── scripts/                   # All my C# scripts
│       ├── Movement.cs           # Player movement
│       ├── ItemCollector.cs      # Coin collection
│       ├── PlayerLoif.cs         # Death and respawn
│       ├── WaypointFollower.cs   # Enemy AI
│       ├── NextLevel.cs          # Level progression
│       ├── StartMenu.cs          # Menu navigation
│       ├── EndMenu.cs            # End screen
│       ├── PlatStick.cs          # Platform sticking
│       └── Rotation.cs           # Object rotation
│
├── Packages/                       # Unity's package dependencies
├── ProjectSettings/                # Unity project configuration
└── UserSettings/                   # My editor preferences
```

I like keeping scripts in their own folder, and organizing assets by type. It makes everything easier to find!

---

## 🛠️ Technologies I Used

### Core Technologies
- **Unity Engine** - 2022.3.26f1 (LTS version - I wanted something stable!)
- **C#** - My programming language of choice
- **Universal Render Pipeline (URP)** - Modern rendering for better graphics

### Unity Systems I Leveraged
- **Physics System** - Rigidbody, Colliders, and Triggers for all the game interactions
- **Input System** - Legacy Input Manager (simple and reliable)
- **Scene Management** - For loading levels seamlessly
- **Audio System** - AudioSource components for all the sound effects
- **UI System** - Canvas and Text components for the HUD and menus
- **Transform System** - Parenting for the platform mechanics

### Development Tools
- **Unity Editor** - Where I spent most of my time!
- **Visual Studio / VS Code** - For writing and debugging my C# code
- **Unity Package Manager** - To manage all the dependencies

---

## 🚀 Let's Get You Started

Want to try out my game? Here's how to get it running on your machine!

### What You'll Need
- **Unity Hub** - Download it from Unity's website if you don't have it
- **Unity Editor** - Version 2022.3.26f1 (or any compatible LTS version)
- **Git** - Optional, but useful if you want to clone the repository

### Step-by-Step Setup

1. **Get the Project**
   ```bash
   git clone [repository-url]
   cd Coin-Quest-A-3D-Platformer-ProtoType
   ```
   Or just download the ZIP and extract it!

2. **Open in Unity**
   - Launch Unity Hub
   - Click "Add" and navigate to the project folder
   - Make sure you have Unity 2022.3.26f1 installed (or let Unity download it)
   - Click "Open" to load the project

3. **Let Unity Import Everything**
   - Unity will automatically import all the assets
   - This might take a minute or two - grab a coffee! ☕
   - Check the Console for any errors (there shouldn't be any!)

4. **Check Build Settings**
   - Go to `File > Build Settings`
   - Make sure your scenes are in this order:
     1. startScreen
     2. 01
     3. 02
     4. endScreen
   - If they're not, just drag them into the right order!

5. **Play the Game!**
   - Press the Play button in Unity Editor
   - Use WASD or Arrow Keys to move
   - Press Space to jump
   - Collect coins and reach the goal!

### Building the Game

Want to build a standalone version? Here's how:

1. **Choose Your Platform**
   - Go to `File > Build Settings`
   - Select your target platform (Windows, Mac, Linux, etc.)

2. **Configure Settings** (Optional)
   - Click "Player Settings"
   - Set your resolution, icon, company name, etc.

3. **Build It!**
   - Click "Build" or "Build and Run"
   - Choose where you want the build saved
   - Wait for Unity to compile everything
   - Done! You now have a playable game!

---

## 🎨 Why I Made These Design Choices

Let me explain some of the decisions I made and why I think they work well:

### Movement System
**What I Did**: Physics-based movement using Rigidbody velocity manipulation.

**Why**: I wanted the movement to feel natural and responsive. Using Unity's physics system means I get realistic gravity, momentum, and collision handling for free. Plus, it makes tweaking movement parameters super easy - just change a few values in the Inspector!

### Ground Detection
**What I Did**: Sphere cast from a dedicated ground check point.

**Why**: I tried simpler methods first, but they had issues. Sometimes the player could jump off walls, or couldn't jump when standing on the edge of a platform. The sphere cast method is more accurate and prevents false positives. It's also performant - just one check per frame!

### Death System
**What I Did**: Delayed respawn (1.3 seconds) with visual and audio feedback.

**Why**: Instant respawns feel jarring and don't give players time to process what happened. The delay lets you see and hear the death feedback, making the experience feel more polished. Plus, it gives a moment to mentally prepare for the retry!

### Waypoint AI
**What I Did**: Array-based waypoint system with distance thresholding.

**Why**: It's simple, flexible, and easy to set up in the Unity Editor. I can create complex patrol paths just by placing empty GameObjects in the scene. The distance threshold (0.1 units) ensures enemies don't get stuck trying to reach exact positions.

### Platform Sticking
**What I Did**: Transform parenting instead of manual position tracking.

**Why**: Unity's transform system is optimized and handles all the edge cases automatically. If I tried to manually calculate positions, I'd have to handle rotation, scaling, and frame-rate issues myself. Why reinvent the wheel when Unity does it better?

---

## 🔮 What I Want to Add Next

I have so many ideas for improvements! Here's my wishlist:

### Gameplay Features
- [ ] **New Input System** - Migrate to Unity's modern Input System for better cross-platform support
- [ ] **Coin Persistence** - Save your coin count across levels or game sessions
- [ ] **Pause Menu** - Add a pause feature with options
- [ ] **Level Select** - Let players choose which level to play after completing the game
- [ ] **Score System** - Time-based scoring and maybe even leaderboards!
- [ ] **Particle Effects** - Visual feedback when collecting coins or defeating enemies
- [ ] **Checkpoint System** - Respawn points throughout levels instead of restarting from the beginning
- [ ] **Settings Menu** - Volume controls, graphics options, etc.
- [ ] **Mobile Support** - Touch controls and mobile optimizations

### Code Improvements
- [ ] **Fix the Typo** - Rename `PlayerLoif` to `PlayerLife` (I know, I know!)
- [ ] **Better Comments** - Add more comprehensive comments throughout the code
- [ ] **ScriptableObjects** - Use them for game configuration data
- [ ] **Event System** - Implement Unity Events for better communication between scripts
- [ ] **Game Manager** - Create a singleton pattern for global state management

If you have suggestions, feel free to let me know! I'm always looking to improve.

---

## 📝 License

This project is available for portfolio demonstration purposes. The audio assets I used are from Casual Game BGM Pack #5 (free use, no resale).

---

## 👤 About This Project

This project represents my journey in Unity 3D game development. I learned so much while building it - from physics-based movement to AI systems to scene management. It's been an incredible learning experience, and I'm excited to share it with you!

---

## 🙏 Acknowledgments

This project was made with the tutorial of a YouTuber named **Florian**, and I am thankful for his tutorials. He was my 2nd and most favorite teacher in my game dev journey. His clear explanations and step-by-step guidance helped me understand not just how to code, but why certain approaches work better than others. Thank you, Florian!

---

<div align="center">

**Built with ❤️ using Unity**

*Last Updated: 2024*

*Thanks for checking out my project! If you have questions or feedback, feel free to reach out!*

</div>
