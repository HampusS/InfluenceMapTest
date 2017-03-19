using InfluenceMapTest.GameObjects;
using InfluenceMapTest.MapFiles;
using InfluenceMapTest.MapFiles.Maps;
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
        BlockedInfluenceMap blockedMap;
        InfluenceMap positiveInfluenceMap, negativeInfluenceMap;
        public static MouseState mouse = Mouse.GetState(), oldMouse;
        public static KeyboardState kbd = Keyboard.GetState(), oldKbd;

        List<Positive> positiveObjects;
        List<Negative> negativeObjects;
        int objSelect = 0;

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
            positiveInfluenceMap = new InfluenceMap(CreatePixel());
            negativeInfluenceMap = new InfluenceMap(CreatePixel());
            blockedMap = new BlockedInfluenceMap(CreatePixel());

            positiveObjects = new List<Positive>();
            negativeObjects = new List<Negative>();

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

            if (kbd.IsKeyDown(Keys.D1) && oldKbd.IsKeyUp(Keys.D1))
                objSelect = 1;
            else if (kbd.IsKeyDown(Keys.D2) && oldKbd.IsKeyUp(Keys.D2))
                objSelect = 2;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //ADD OBJECTS
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                if (blockedMap.CheckVacancy(mouse.Position))
                {
                    if (blockedMap.GetCellPosition(mouse.Position) != Point.Zero)
                    {
                        switch (objSelect)
                        {
                            case 1:
                                positiveObjects.Add(new Positive(CreatePixel(), positiveInfluenceMap.GetCellPosition(mouse.Position)));
                                positiveInfluenceMap.CalculateInfluenceFromObject(positiveObjects[positiveObjects.Count - 1]);
                                break;
                            case 2:
                                negativeObjects.Add(new Negative(CreatePixel(), negativeInfluenceMap.GetCellPosition(mouse.Position)));
                                negativeInfluenceMap.CalculateInfluenceFromObject(negativeObjects[negativeObjects.Count - 1]);
                                break;
                        }
                        blockedMap.SetCellOccupancy(mouse.Position, true);
                    }
                }
            }


            // ON INDIVIDUAL REMOVAL
            SelectObjToRemove();
            ClearMap();

            base.Update(gameTime);
        }

        /// <summary>
        /// Selects an individual tile and removes the obj in that tile
        /// </summary>
        private void SelectObjToRemove()
        {
            if (mouse.RightButton == ButtonState.Pressed)
            {
                if (!blockedMap.CheckVacancy(mouse.Position))
                {
                    for (int i = positiveObjects.Count; i >= 0; --i)
                    {
                        if (positiveObjects[i].myCellIsSelected(positiveInfluenceMap.GetCellFromPoint(mouse.Position)))
                        {
                            positiveObjects.RemoveAt(i);
                            blockedMap.SetCellOccupancy(mouse.Position, false);
                        }
                    }

                    for (int i = 0; i < negativeObjects.Count; i++)
                    {
                        if (negativeObjects[i].myCellIsSelected(positiveInfluenceMap.GetCellFromPoint(mouse.Position)))
                        {
                            negativeObjects.RemoveAt(i);
                            blockedMap.SetCellOccupancy(mouse.Position, false);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Clears The entire influence map and game objects
        /// </summary>
        private void ClearMap()
        {
            if (kbd.IsKeyDown(Keys.R) && oldKbd.IsKeyUp(Keys.R))
            {
                for (int i = negativeObjects.Count - 1; i >= 0; --i)
                {
                    negativeObjects.RemoveAt(i);
                }
                for (int i = positiveObjects.Count - 1; i >= 0; --i)
                {
                    positiveObjects.RemoveAt(i);
                }                
                blockedMap.ClearOccupancy();
                positiveInfluenceMap = new InfluenceMap(CreatePixel());
                negativeInfluenceMap = new InfluenceMap(CreatePixel());
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            positiveInfluenceMap.Draw(spriteBatch);
            negativeInfluenceMap.Draw(spriteBatch);
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
