using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;

namespace Duel2D
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private int schermata = 0;
        public tcpClass clientTcp { get; set; }
        public bool con = false;

        Screen screen;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            screen = new Screen();
     
            /*
            Thread connectionThread = new Thread(ConnessioneServer);
            connectionThread.Start();
            */
            base.Initialize();
        }

        private void ConnessioneServer()
        {
            clientTcp = new tcpClass("172.16.102.109", 666);
            con = clientTcp.connettiServer();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            screen.carica(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (schermata == 0 && Keyboard.GetState().GetPressedKeys().Length > 0)    //quando l'utente preme un qualsiasi tasto dalla schermata iniziale passa al menu
                schermata = 1;

            if (schermata == 1)
                screen.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if(schermata == 0)
            {
                screen.DrawStart(spriteBatch);
            }
            else if (schermata == 1)
            {
                screen.DrawMenu(spriteBatch);
            }

            base.Draw(gameTime);
        }
    }
}