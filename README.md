# twin-stick
A twin stick shooter mobile game prototype.

For testing, I using the Unity Remote 5 app for iOS.

### 22 Jan 2020
This project and playing a lot of the game Devil Daggers and Dark Souls has inspired me to make a FPS game. I will come back to this eventually, but since project has ended up in a pretty good spot, I am fine stopping development on this for now.

### 14 Jan 2020
Units (and thus the player) now deal damage by firing their bullets. The current health of a unit is displayed as bar, where the vibrant green gets smaller as the units health decreases.
Goals for the next week include...
- (Major) Create a class to represent skills (will make it easy to switch out skills for a unit)
- (Major) Make a melee primary attack
- ~~(Major) Have some basic "AI" for enemies (will elaborate on later)~~ Enemies will follow you around to stay in range to shoot at you. Once in range, they'll stay away from you as much as possible.
- ~~(Major) Create a prototype where enemies infinitely spawn and you fight them~~ Game spawns a new unit every couple of seconds in a random location in the game.
- (Major) When dealing damage, have a number show the amount of damage they lossed and it hop off
- (Major) When you tap on a unit, have a UI in the corner display its stats
- (Major) Have the main HUD display unit health, information, and skills
- ~~(Minor) Switch bullets from having a lifetime to having a max travel distance~~ Having a life time would be useful in cases where trajectories are not straight lines. Can easily calculate the range of a bullet.
- (Minor) Figure out the Quaternion problem with unit rotation
- (Minor) Add text to health bar UI to display amount of hit points left

### 10 Jan 2020
When player aims, they will fire bullets automatically. Next is to create enemies and health, and then implement damage.

### 08 Jan 2020
Created initial repository. Developed basic controls. Next goal is to get basic shooting down.
