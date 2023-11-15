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
        public Texture2D sIniziale { get; set; }
        public Texture2D sMenu { get; set; }
        public SpriteFont fAll { get; set; }
        public tcpClass clientTcp { get; set; }
        public menu menu { get; set; }
        public giocatore giocatore { get; set; }
        

        private float opacitaTI = 1f;
        private bool sensoOpacita = true;
        

        public Screen()
        {
            clientTcp = new tcpClass("127.0.0.1", 666);
            menu = new menu();
        }

        public void carica(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            sIniziale = content.Load<Texture2D>("sIniziale");
            sMenu = content.Load<Texture2D>("sMenu");
            fAll = content.Load<SpriteFont>("fAll");
            clientTcp.carica(content);
            menu.carica(content);
        }

        public void unload()
        {

        }

        public int Update(GameTime gameTime)
        {
            clientTcp.Update();
            

            //if menu update = 2 allora salva giocatore, getGiocatore

            int nr = menu.Update(gameTime);
            if(nr == 2)
            {
                giocatore = menu.getGiocatore();
                return 2;
            }
            return nr;
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
            spriteBatch.Begin();
            spriteBatch.Draw(sIniziale, new Rectangle(0, 0, 1200, 800), Color.White);
            spriteBatch.DrawString(fAll, "premi un qualsiasi pulsante per continuare", new Vector2(250, 620), Color.White * opacitaTI);
            spriteBatch.End();
        }
    }
}
