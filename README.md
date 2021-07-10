# Froggies Jump
Froggies Jump is semi-race multiplayer game online where you playing as a frog who trying to become the fastest frog in the city. This game can be played up to 10 or more player in one sessions as long as it's not ended. If the game sessions already have a winner, you should restart the game to play with your friend again. It's simple yet fun to test your friendship and competitiveness.

## How To Play
**1. Log In.**

![1 Log In](https://user-images.githubusercontent.com/74699417/125155183-4850f880-e188-11eb-8ff1-d04a00c096e8.gif)

If you don't have account before, don't worry! Simply input your email and password then our server will sign you up automatically but please remember the password that linked to that email so you can play with the same account later.

**2. Optimize your game before playing in main menu and start the game and choose level in lobby.**

![2 Main Menu](https://user-images.githubusercontent.com/74699417/125155206-78989700-e188-11eb-95cf-a520670f0cb7.gif)

You can adjust your playing experience in main menu there's some features like settings (where you can setting music volume, etc), account (to change your account), etc. If you already comfy with the setup, then you can click start. In the lobby, you can choose which level you want to play with your friends. We recommend using 3rd party voice call platform like Discord to have better play experience.

**3. Compete with your friend.**

![3 Game Scene](https://user-images.githubusercontent.com/74699417/125155258-bbf30580-e188-11eb-846c-ca34b0658c2e.gif)

After choosing level, the game started! Compete with your friend by **_moving your frog character with left and right arrow keyboard button and space to jump_**.

**4. Win the race!**

![4 Game Finish](https://user-images.githubusercontent.com/74699417/125155264-c6ad9a80-e188-11eb-821a-82b6ff5606e2.gif)

Once you arrive at the finish line, game sessions will end and show who the real winner of the race. Congrats! You become the fastest frog in the city!

## Game Project Details

**1. Deployment Diagram**

![Deploy](https://user-images.githubusercontent.com/74699417/125155994-5ead8300-e18d-11eb-98c6-67f10fd2b0ed.png)

**2. Architecture Diagram**

![Archi](https://user-images.githubusercontent.com/74699417/125156012-771d9d80-e18d-11eb-8e54-a8d43c2e4963.png)


**3. Frame work.**

For this project we use two frame work, [Colyseus](https://www.colyseus.io/) and [Express](https://docs.nestjs.com/). Both of them are built in Node.JS and the best choose to make multiplayer game online.

**4. Database.**

For database, we use [MongoDB](https://mongodb.com/) that good for modern application developers and for the cloud era.

**5. Authentication**

To identify player, we use authentication by [JWT](https://jwt.io/) that good for daily single uses token.
