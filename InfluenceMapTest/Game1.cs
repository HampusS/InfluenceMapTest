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

        //List<GameObject> objects;
        List<Positive> posObj;
        List<Negative> negObj;
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
            map = new Map(CreatePixel());
            //objects = new List<GameObject>();

            posObj = new List<Positive>();
            negObj = new List<Negative>();

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

            if (kbd.IsKeyDown(Keys.D1) && oldKbd.IsKeyUp(Keys.D1))
                objSelect = 1;
            else if (kbd.IsKeyDown(Keys.D2) && oldKbd.IsKeyUp(Keys.D2))
                objSelect = 2;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            map.Update();

            //ADD OBJECTS
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                if (map.CheckVacancy(mouse.Position))
                {
                    if (map.GetCellPosition(mouse.Position) != Vector2.Zero)
                    {
                        switch (objSelect)
                        {
                            case 1:
                                posObj.Add(new Positive(CreatePixel(), map.GetCellPosition(mouse.Position)));
                                map.SetCellOccupancy(mouse.Position, true);
                                for (int i = 0; i < posObj.Count; i++)
                                {
                                    map.CalculateInfluenceFromObject(posObj[i]);
                                }
                                break;
                            case 2:
                                negObj.Add(new Negative(CreatePixel(), map.GetCellPosition(mouse.Position)));
                                map.SetCellOccupancy(mouse.Position, true);
                                for (int j = 0; j < negObj.Count; j++)
                                {
                                    map.CalculateInfluenceFromObject(negObj[j]);
                                }
                                break;
                        }

                    }
                }
            }




            // ON INDIVIDUAL REMOVAL
            SelectObjToRemove();

            ClearMap();

            base.Update(gameTime);
        }

        private void SelectObjToRemove()
        {
            if (mouse.RightButton == ButtonState.Pressed)
            {
                if (!map.CheckVacancy(mouse.Position))
                {
                    for (int i = posObj.Count; i >= 0; --i)
                    {
                        if (posObj[i].myCellIsSelected(map.GetCellFromPoint(mouse.Position)))
                        {
                            posObj.RemoveAt(i);
                            map.SetCellOccupancy(mouse.Position, false);
                        }
                    }

                    for (int i = 0; i < negObj.Count; i++)
                    {
                        if (negObj[i].myCellIsSelected(map.GetCellFromPoint(mouse.Position)))
                        {
                            negObj.RemoveAt(i);
                            map.SetCellOccupancy(mouse.Position, false);
                        }
                    }
                }
            }
        }

        private void ClearMap()
        {
            if (kbd.IsKeyDown(Keys.R) && oldKbd.IsKeyUp(Keys.R))
            {
                for (int i = negObj.Count - 1; i >= 0; --i)
                {
                    negObj.RemoveAt(i);
                }
                for (int i = posObj.Count - 1; i >= 0; --i)
                {
                    posObj.RemoveAt(i);
                }
                map.ClearOccupancy();
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
            map.Draw(spriteBatch);
            //foreach (GameObject obj in objects)
            //{
            //    //obj.Draw(spriteBatch);
            //}
            spriteBatch.End();

            // TODO: Add your drawing code here

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
