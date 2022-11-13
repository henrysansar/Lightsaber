# Lightsaber
A full scripted and realist SW Lightsaber - contains mesh, animations, textures, sound fxs and the script

# Features
- Animated start, just like in the movies, containing the classic sound
- When a saber collides with other sabers or any other solid object, this will throw sounds and light FXs
- Many sounds included such as hit sounds, light start sound and humming sound
- Will not trigger sounds when in off state
- Not suitable for avatar accessory - Build for experiences
- Easy to customize colors

# How to Customize Colors

- Oh the property sheet of the object, there is a property called Reflex Color : Change to the color that you wish
- Edit the materials and select the material called Light and put the very same color you defined as Reflex Color

# How to Place it on Sansar

- Upload the FBX (Mesh and Collider)
- Upload all the wav/mp3 files
- Upload the script file
- There is a file called Sansar Object Properties, so try to use this as guidance and apply the properties
- You need to include the grab points, light and Audio component into the object container
- Audio Properties: Sphere 0.5 Radius, 0 db Loudness
- Light is point, position of 0,0,1.255, range of 5 and don't forget to mark it as scriptable