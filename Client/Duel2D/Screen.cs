using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Reflection.Metadata;
using System.Net.Sockets;
using System.Threading;
using Microsoft.Xna.Framework.Input;

namespace Duel2D
{
    internal class Screen
    {
        public int schermata = 0;
        public Texture2D sIniziale { get; set; }
        public Texture2D sMenu { get; set; }
        public Texture2D sCaricamento { get; set; }
        public Texture2D ruotaCaricamento { get; set; }
        public SpriteFont fAll { get; set; }
        public tcpClass clientTcp { get; set; }
        public menu menu { get; set; }
        public giocatore giocatore { get; set; }
        public giocatore avversario { get; set; }
        public animazione caricamento { get; set; }
        

        private float opacitaTI = 0.1f;
        private bool sensoOpacita = true;
        private int nr = 1;
        private int i = 0;
        

        public Screen()
        {
            clientTcp = new tcpClass("127.0.0.1", 9999);
            menu = new menu();
        }

        public void carica(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            sIniziale = content.Load<Texture2D>("sIniziale");
            sMenu = content.Load<Texture2D>("sMenu");
            sCaricamento = content.Load<Texture2D>("sCaricamento");
            ruotaCaricamento = content.Load<Texture2D>("caricamento");
            fAll = content.Load<SpriteFont>("fAll");
            clientTcp.carica(content);
            menu.carica(content);
            caricamento = new animazione(ruotaCaricamento, 1 , 5, 2, 220);
        }

        public void unload()
        {

        }

        public void updateStart(GameTime gameTime)
        {

        }

        public void updateMenu(GameTime gameTime)
        {
            clientTcp.Update();
            menu.Update(gameTime);
            giocatore = menu.getGiocatore();
            if(menu.isGioca())
                schermata = 2;
        }

        public void updateCaricamento(GameTime gameTime)
        {
            caricamento.Update(gameTime);
        }

        public void animazioneTesto()
        {
            if (sensoOpacita)
            {
                opacitaTI += 0.02f;
                if (opacitaTI >= 1.0f)
                {
                    opacitaTI = 1.0f;
                    sensoOpacita = false;
                }
            }
            else
            {
                opacitaTI -= 0.02f;
                if (opacitaTI <= 0.1f)
                {
                    opacitaTI = 0.1f;
                    sensoOpacita = true;
                }
            }
        }

        public void DrawStart(SpriteBatch spriteBatch)
        {
            animazioneTesto();

            spriteBatch.Begin();
            spriteBatch.Draw(sIniziale, new Rectangle(0, 0, 1200, 800), Color.White);
            spriteBatch.DrawString(fAll, "premi un qualsiasi pulsante per continuare", new Vector2(250, 620), Color.White * opacitaTI);
            spriteBatch.End();
        }

        public void DrawMenu(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            spriteBatch.Draw(sMenu, new Rectangle(0, 0, 1200, 800), Color.White);
            clientTcp.DrawStatus(spriteBatch);

            menu.DrawMenu(spriteBatch);
            

            spriteBatch.End();
        }

        public void DrawCaricamento(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(sCaricamento, new Rectangle(0, 0, 1200, 800), Color.White * 0.16f);
            caricamento.Draw(spriteBatch, new Vector2(552, 352));

            clientTcp.invia(giocatore.toCsv());
            if(!clientTcp.isRicevendo())
                avversario = giocatore.toGiocatore(clientTcp.ricevi());

            spriteBatch.End();
        }

        internal int getSchermata()
        {
            return schermata;
        }

        internal int setSchermata(int x)
        {
            schermata = x;
            return schermata;
        }
    }
}
