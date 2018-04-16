using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace BOB2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        
       
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
            GameElements.state = GameElements.State.Menu;
            GameElements.Initialize();
       
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            GameElements.LoadContent(Content, Window);
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

            switch(GameElements.state)
            {
                case GameElements.State.HighScore:
                    IsMouseVisible = true;
                    GameElements.state = GameElements.HighScoreUpdate();
                    break;
                case GameElements.State.Run:
                    IsMouseVisible = false;
                    GameElements.state = GameElements.Update(Content,Window,gameTime);
                    break;

                case GameElements.State.Menu:
                    IsMouseVisible = true;
                    GameElements.state = GameElements.MenuUpdate();
                    break;
                case GameElements.State.Loading:
                    GameElements.state = GameElements.LoadingUpdate(Content,gameTime);
                    break;
                case GameElements.State.Skriv:
                    GameElements.state = GameElements.SkrivUpdate();
                    break;
                case GameElements.State.Information:
                    GameElements.state = GameElements.InformationUpdate();
                    break;
                default:
                    Exit();
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
           
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            switch(GameElements.state)
            {
                case GameElements.State.Run:
                    GameElements.Draw(spriteBatch);
                    break;
                case GameElements.State.Menu:
                    GameElements.MenuDraw(spriteBatch);
                    break;
                case GameElements.State.HighScore:
                    GameElements.HighScoreDraw(spriteBatch);
                    break;
                case GameElements.State.Loading:
                    GameElements.LoadingDraw(spriteBatch);
                    break;
                case GameElements.State.Skriv:
                    GameElements.SkrivDraw(spriteBatch);
                    break;
                case GameElements.State.Information:
                    GameElements.InformationDraw(spriteBatch);
                    break;
                default:
                    Exit();
                    break;
            }
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
