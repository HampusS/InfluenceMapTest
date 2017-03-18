using InfluenceMapTest.GameObjects;
using InfluenceMapTest.MapFiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace InfluenceMapTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Map map;
        public static MouseState mouse = Mouse.GetState(), oldMouse;
        public static KeyboardState kbd = Keyboard.GetState(), oldKbd;

        List<GameObject> objects;

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
            map = new Map(CreatePixel(Color.White), 47, 28, 16);
            objects = new List<GameObject>();
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

            // TODO: use this.Content to load your game content here
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            map.Update();
            // TODO: Add your update logic here
            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
            {
                if (map.CheckVacancy(mouse.Position))
                {
                    objects.Add(new GameObject(CreatePixel(Color.LightGreen), map.GetCellPosition(mouse.Position)));
                    map.SetCellOccupancy(mouse.Position, true);
                }
            }

            if (mouse.RightButton == ButtonState.Pressed && oldMouse.RightButton == ButtonState.Released)
            {
                if (!map.CheckVacancy(mouse.Position))
                {
                    for (int i = objects.Count - 1; i >= 0; --i)
                    {
                        if (objects[i].myCellIsSelected(map.GetCell(mouse.Position)))
                        {
                            objects.RemoveAt(i);
                            map.SetCellOccupancy(mouse.Position, false);
                        }
                    }
                }
            }

            if (kbd.IsKeyDown(Keys.R) && oldKbd.IsKeyUp(Keys.R))
            {
                for (int i = objects.Count - 1; i >= 0; --i)
                {
                    objects.RemoveAt(i);
                    map.ClearOccupancy();
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            map.Draw(spriteBatch);
            foreach (GameObject obj in objects)
            {
                obj.Draw(spriteBatch);
            }
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        Texture2D CreatePixel(Color color)
        {
            var rect = new Texture2D(GraphicsDevice, 1, 1);
            rect.SetData(new[] { color });
            return rect;
        }
    }
}
