using InfluenceMapTest.GameObjects;
using InfluenceMapTest.MapFiles;
using InfluenceMapTest.MapFiles.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace InfluenceMapTest
{

    public struct InfluenceMapConfig
    {
        public static int CellWidth = 16;
        public static int CellHeight = 16;
        public static int MapWidth = 50;
        public static int MapHeight = 30;
        public static float FallOff = 0.8f;
    }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static KeyboardState kbd = Keyboard.GetState(), oldKbd;
        public static MouseState mouse = Mouse.GetState(), oldMouse;

        InfluenceMap positiveInfluenceMap, negativeInfluenceMap;
        BlockedInfluenceMap blockedMap;
        FinalInfluenceMap influenceMap;

        List<GameObject> positiveObjects;
        List<GameObject> negativeObjects;
        List<GameObject> gameObjects;

        int objSelect = 1;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            positiveInfluenceMap = new InfluenceMap(CreatePixel(), Color.SkyBlue);
            negativeInfluenceMap = new InfluenceMap(CreatePixel(), Color.MonoGameOrange);
            blockedMap = new BlockedInfluenceMap(CreatePixel(), Color.Violet);
            influenceMap = new FinalInfluenceMap(CreatePixel(), Color.Black);

            positiveObjects = new List<GameObject>();
            negativeObjects = new List<GameObject>();
            gameObjects = new List<GameObject>();

            IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
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
            oldMouse = mouse;
            mouse = Mouse.GetState();
            oldKbd = kbd;
            kbd = Keyboard.GetState();

            foreach (GameObject obj in positiveObjects)
            {
                obj.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            foreach (GameObject obj in negativeObjects)
            {
                obj.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            positiveInfluenceMap.Update(positiveObjects);
            negativeInfluenceMap.Update(negativeObjects);
            blockedMap.Update(gameObjects);

            if (kbd.IsKeyDown(Keys.D1) && oldKbd.IsKeyUp(Keys.D1))
                objSelect = 1;
            else if (kbd.IsKeyDown(Keys.D2) && oldKbd.IsKeyUp(Keys.D2))
                objSelect = 2;

            if (mouse.LeftButton == ButtonState.Pressed)
                AddGameObject();
            else if (kbd.IsKeyDown(Keys.R) && oldKbd.IsKeyUp(Keys.R))
                ClearMap();

            influenceMap.FinalizeInfluence(positiveInfluenceMap, negativeInfluenceMap);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            base.Update(gameTime);
        }

        private void AddGameObject()
        {
            if (blockedMap.GetCell(mouse.Position) != null)
            {
                if (blockedMap.CheckVacancy(mouse.Position))
                {
                    switch (objSelect)
                    {
                        case 1:
                            positiveObjects.Add(new GameObject(CreatePixel(), positiveInfluenceMap.GetCell(mouse.Position).GetPosition(), Color.LimeGreen, InfluenceMapConfig.CellWidth));
                            gameObjects.Add(positiveObjects[positiveObjects.Count - 1]);
                            break;
                        case 2:
                            negativeObjects.Add(new GameObject(CreatePixel(), negativeInfluenceMap.GetCell(mouse.Position).GetPosition(), Color.HotPink, InfluenceMapConfig.CellWidth));
                            gameObjects.Add(negativeObjects[negativeObjects.Count - 1]);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Clears The entire influence map and game objects
        /// </summary>
        private void ClearMap()
        {
            negativeObjects.Clear();
            positiveObjects.Clear();
            gameObjects.Clear();
            blockedMap = new BlockedInfluenceMap(CreatePixel(), Color.Black);
            positiveInfluenceMap = new InfluenceMap(CreatePixel(), Color.SkyBlue);
            negativeInfluenceMap = new InfluenceMap(CreatePixel(), Color.MonoGameOrange);
            influenceMap = new FinalInfluenceMap(CreatePixel(), Color.Black);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            influenceMap.Draw(spriteBatch);
            blockedMap.Draw(spriteBatch);
            foreach (GameObject obj in positiveObjects)
            {
                obj.Draw(spriteBatch);
            }
            foreach (GameObject obj in negativeObjects)
            {
                obj.Draw(spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        Texture2D CreatePixel()
        {
            var rect = new Texture2D(GraphicsDevice, 1, 1);
            rect.SetData(new[] { Color.White });
            return rect;
        }
    }
}
