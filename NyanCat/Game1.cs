using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NyanCat
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public enum Screens
        {
            Greeting,
            Game,
            Pause,
            GameOver
        }
        private Screens currentScreen = Screens.Greeting;

        public static int screenWidth;
        public static int screenHeight;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Background background;
        private NyanCat nyanCat;
        public List<float> Locations { get; private set; }
        private Rainbow rainbow;
        private List<Platform> platforms;
        private List<Thing> Things;
        private int ItemsSpeed = 3;
        private int BackgrSpeed = 2;
        public string[] PlatformsMap { get; private set; }

        public int GameScores;

        private SpriteFont scoresFont;
        private SpriteFont greetingFont;
        private SpriteFont pauseFont;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            screenWidth = graphics.PreferredBackBufferWidth = 800;
            screenHeight = graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            //Random rnd = new Random();
            PlatformsMap = File.ReadAllLines("Content\\map1.txt");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            background = new Background(Content.Load<Texture2D>("backgr"), screenWidth, BackgrSpeed);           
            nyanCat = new NyanCat(Content.Load<Texture2D>("nyacat"), 150, screenHeight / 2);
            rainbow = new Rainbow(Content.Load<Texture2D>("rb1"), Content.Load<Texture2D>("rb2"));
            Locations = new List<float>();
            for (int i = 0; i < 14; i++)
                Locations.Add(nyanCat.NyanPosition.Y);
            LoadMap(Content.Load<Texture2D>("plat"), Content.Load<Texture2D>("Apple"), Content.Load<Texture2D>("peshenie"),
                Content.Load<Texture2D>("tort"), Content.Load<Texture2D>("goldCar"), Content.Load<Texture2D>("tnt"), Content.Load<Texture2D>("empty"));
            /*platforms = new List<Platform>();
            Things = new List<Thing>();
            for (int i = 0; i < PlatformsMap.Length; i++)
            {
                for (int j = 0; j < PlatformsMap[0].Length; j++)
                {
                    if (PlatformsMap[i][j] == '#')
                    {
                        platforms.Add(new Platform(Content.Load<Texture2D>("plat"), 50 * j, 50 * i, screenWidth));
                        var rand = rnd.Next(0, 100);
                        if (rand > 0 && rand < 25)
                            Things.Add(new Thing(Content.Load<Texture2D>("Apple"), Content.Load<Texture2D>("empty"), 
                                50 * j, 50 * i - 50 * (1 + rnd.Next(0, 2)), 100));
                        else if (rand > 24 && rand < 33)
                            Things.Add(new Thing(Content.Load<Texture2D>("peshenie"), Content.Load<Texture2D>("empty"), 
                                50 * j, 50 * i - 50, 300));
                        else if (rand > 32 && rand < 39)
                            Things.Add(new Thing(Content.Load<Texture2D>("tort"), Content.Load<Texture2D>("empty"), 
                                50 * j, 50 * i - 50 * (1 + rnd.Next(0, 2)), 500));
                        else if (rand > 38 && rand < 41)
                            Things.Add(new Thing(Content.Load<Texture2D>("goldcar"), Content.Load<Texture2D>("empty"), 
                                50 * j, 50 * i - 50 * (1 + rnd.Next(0, 2)), 1500));
                        else if (rand > 40 && rand < 47)
                            Things.Add(new Thing(Content.Load<Texture2D>("tnt"), Content.Load<Texture2D>("empty"), 
                                50 * j, 50 * i - 50 * (1 + rnd.Next(0, 2)), -1500));
                    }
                }
            }*/
            scoresFont = Content.Load<SpriteFont>("ScoresFont");
            greetingFont = Content.Load<SpriteFont>("GreetingFont");
            pauseFont = Content.Load<SpriteFont>("PauseFont");
        }

        public void LoadMap (Texture2D platText, Texture2D appleText, Texture2D peshenieText, Texture2D tortText, Texture2D goldCarText, Texture2D tntText, Texture2D emptyText)
        {
            Random rnd = new Random();
            platforms = new List<Platform>();
            Things = new List<Thing>();
            for (int i = 0; i < PlatformsMap.Length; i++)
            {
                for (int j = 0; j < PlatformsMap[0].Length; j++)
                {
                    if (PlatformsMap[i][j] == '#')
                    {
                        platforms.Add(new Platform(platText, 50 * j, 50 * i, screenWidth));
                        var rand = rnd.Next(0, 100);
                        if (rand > 0 && rand < 25)
                            Things.Add(new Thing(appleText, emptyText,
                                50 * j, 50 * i - 50 * (1 + rnd.Next(0, 2)), 100));
                        else if (rand > 24 && rand < 33)
                            Things.Add(new Thing(peshenieText, emptyText,
                                50 * j, 50 * i - 50, 300));
                        else if (rand > 32 && rand < 39)
                            Things.Add(new Thing(tortText, emptyText,
                                50 * j, 50 * i - 50 * (1 + rnd.Next(0, 2)), 500));
                        else if (rand > 38 && rand < 41)
                            Things.Add(new Thing(goldCarText, emptyText,
                                50 * j, 50 * i - 50 * (1 + rnd.Next(0, 2)), 1500));
                        else if (rand > 40 && rand < 47)
                            Things.Add(new Thing(tntText, emptyText,
                                50 * j, 50 * i - 50 * (1 + rnd.Next(0, 2)), -1500));
                    }
                }
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (currentScreen)
            {
                case Screens.Greeting:
                    background.Scroll(screenWidth);
                    rainbow.Scroll(Locations);
                    Locations.Add(nyanCat.NyanPosition.Y);
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        currentScreen = Screens.Game;
                    break;

                case Screens.Game:
                    background.Scroll(screenWidth);
                    rainbow.Scroll(Locations);
                    Locations.Add(nyanCat.NyanPosition.Y);
                    foreach (var platform in platforms)
                    {
                        platform.Scroll(ItemsSpeed);
                    }

                    foreach (var thing in Things)
                    {
                        thing.Scroll(ItemsSpeed);
                    }
             
                    GameScores = nyanCat.NyanCheck(platforms, Things, GameScores);

                    if (Keyboard.GetState().IsKeyDown(Keys.P))
                        currentScreen = Screens.Pause;
                    if (nyanCat.NyanPosition.Y >= screenHeight)
                        currentScreen = Screens.GameOver;
                    break;

                case Screens.Pause:
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        currentScreen = Screens.Game;
                    break;

                case Screens.GameOver:
                        break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            background.DrawBackground(spriteBatch);          
            switch (currentScreen)
            {
                case Screens.Greeting:
                    spriteBatch.DrawString(greetingFont, "Press Space to start", new Vector2(300, 300), Color.White);
                    break;

                case Screens.Game:
                    foreach (var platform in platforms)
                        platform.DrawPlatform(spriteBatch);
                    foreach (var thing in Things)
                        thing.DrawThing(spriteBatch);
                    spriteBatch.DrawString(scoresFont, "Scores: " + GameScores, new Vector2(20, 20), Color.White);
                    spriteBatch.DrawString(scoresFont, "P - Pause", new Vector2(700, 20), Color.White);
                    break;

                case Screens.Pause:
                    foreach (var platform in platforms)
                        platform.DrawPlatform(spriteBatch);
                    foreach (var thing in Things)
                        thing.DrawThing(spriteBatch);
                    spriteBatch.DrawString(pauseFont, "PAUSE", new Vector2(275, 300), Color.White);
                    spriteBatch.DrawString(scoresFont, "Press Space to continue", new Vector2(275, 375), Color.White);
                    spriteBatch.DrawString(scoresFont, "Scores: " + GameScores, new Vector2(20, 20), Color.White);
                    break;

                case Screens.GameOver:
                    spriteBatch.DrawString(pauseFont, "GAME OVER", new Vector2(200, 250), Color.White);
                    spriteBatch.DrawString(greetingFont, "Your score: " + GameScores, new Vector2(250, 350), Color.White);
                    spriteBatch.DrawString(greetingFont, "Press ESC to exit...", new Vector2(240, 450), Color.White);
                    break;
            }
            rainbow.DrawRainbow(spriteBatch);
            nyanCat.DrawNyan(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
