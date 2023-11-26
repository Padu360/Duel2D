using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;

namespace Duel2D
{
    public class Game1 : Game           //classe principale che si occupa di gestire il gioco
    {
        private GraphicsDeviceManager graphics;         //questo si occupa delle impostazioni grafiche e altre impostazioni di default del gioco, come grandezza schermo, mouse ecc.
        private SpriteBatch spriteBatch;                //con la spriteBatch è possibile usare alcuni funzioni base della grafica, come Draw() e DrawString()
        private int schermata = 0;                      //variabile che indica su quale schermata ci troviamo, start, menu, caricamento, gameplay

        Screen screen;                                  //creo l'oggetto screen che si occuperà di gestire tutte le schermate

        public Game1()                                  //nel costruttore della glasse Game1 si impostano le impostazioni desiderate
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 800;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()            //Initializa viene eseguita una sola volta, ed esegue quello che si trova al suo interno una sola volta
        {
            screen = new Screen();
            base.Initialize();
        }

        protected override void LoadContent()           //con la loadContent si caricano le texture e i font, viene richiamata anche nelle altri classi
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            screen.carica(Content);
        }

        protected override void Update(GameTime gameTime)   //la funzione Update(), viene chiamata ogni tot tempo, es 60 volte al secondo se il gioco gira a 60 fps
        {
            if (schermata == 0 && (Keyboard.GetState().GetPressedKeys().Length > 0 || Mouse.GetState().LeftButton == ButtonState.Pressed || Mouse.GetState().RightButton == ButtonState.Pressed))
            {   //quando l'utente preme un qualsiasi tasto (anche del mouse) dalla schermata iniziale passa al menu
                screen.updateStart(gameTime);
                schermata = screen.setSchermata(1);
            }

            if (schermata == 1)
            {
                screen.updateMenu(gameTime);
            }

            if (schermata == 2)
            {
                screen.updateCaricamento(gameTime);
                schermata = screen.setSchermata(2);
            }

            if (schermata == 3)
            {
                schermata = screen.setSchermata(3);
                screen.updateGioco(gameTime);               //ogni schermata al proprio update per un discorso di ordine, e perchè no, anche di ottimizzazione 
            }

            schermata = screen.getSchermata();          
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) //questa funzione, per disegnare, viene chiamata dopo che la funzione Update() è stata chiamata
        {
            if (schermata == 0)
            {
                screen.DrawStart(spriteBatch);
            }
            else if (schermata == 1)
            {
                screen.DrawMenu(spriteBatch);
            }
            else if (schermata == 2)
            {
                screen.DrawCaricamento(spriteBatch);
            }
            else if (schermata == 3)
            {
                screen.DrawGioco(spriteBatch);              //in questo caso ovviamente ogni schermata a la propria funzione di Draw
            }

            base.Draw(gameTime);
        }
    }
}