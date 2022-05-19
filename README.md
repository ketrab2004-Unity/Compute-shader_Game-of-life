# Compute Shader Powered Game of Life

![Scene view of the camera pointing towards the Game of Life plane, (black because it has been run once)](./memorabilia/scene_black.png)

![Ingame view of the first step of the Game of Life process, the chosen image being loaded in (my dog)](./memorabilia/game_photo.png)
![Ingame view of the first image after being converted to black and white, with some pixels still being gray (idk)](./memorabilia/game_photo_black-and-white.png)
![Ingame view of a bunch of iterations later of the starting image](./memorabilia/game_photo_game-of-life-ified.png)

![Scene view closeup of the "processed" Game of Life image](./memorabilia/scene_game_photo_closeup.png)

###### I saw a bunch of Unity videos by [Sebastion Lague](https://www.youtube.com/c/SebastianLague), he uses compute shaders a lot to process or pre-process data. So i wanted to try something similar

<hr/>

![Editor settings menu to control the Game of Life Compute Shader](./memorabilia/editor_game-of-life_settings.png)
![Editor settings menu like above, but ingame. So with default values filled in and readonly values also filled in](./memorabilia/editor_game-of-life_settings_ingame.png)
###### Above is the editor view of the Game of Life controller script
###### You can set the starting texture and how big the canvas should be
###### You can pause it and change how long it should wait before doing the next step
###### And lastly you can see the framerate of the shader
