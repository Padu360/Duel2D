using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Reflection.Metadata;
using System.Net.Sockets;

namespace Duel2D
{
    internal class Screen
    {
        public Texture2D sIniziale { get; set; }
        public Texture2D sMenu { get; set; }
        public Texture2D sNome { get; set; }
        public SpriteFont fAll { get; set; }
        public tcpClass clientTcp { get; set; }

        private float opacitaTI = 1f;
        private bool sensoOpacita = true;

        public Screen()
        {
            clientTcp = new tcpClass("172.16.102.109", 666);
        }

        public void carica(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            sIniziale = content.Load<Texture2D>("sIniziale");
            sMenu = content.Load<Texture2D>("sMenu");
            sNome = content.Load<Texture2D>("nome2");
            fAll = content.Load<SpriteFont>("fAll");
        }

        public void unload()
        {

        }

        public void Update()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            
            


            spriteBatch.End();
        }

        public void animazioneTesto()
        {
            if (sensoOpacita)
            {
                opacitaTI += 0.03f;
                if (opacitaTI >= 1.0f)
                {
                    opacitaTI = 1.0f;
                    sensoOpacita = false;
                }
            }
            else
            {
                opacitaTI -= 0.03f;
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
            clientTcp.connettiServer();
            clientTcp.inviaMessaggio("Hello, server!");



            spriteBatch.Begin();
            spriteBatch.Draw(sMenu, new Rectangle(0, 0, 1200, 800), Color.White);
            spriteBatch.Draw(sMenu, new Rectangle(10, 10, 80, 35), Color.White);
            spriteBatch.Draw(sNome, new Rectangle(500, 100, 190, 80), Color.White);
            spriteBatch.End();
        }

        public void DrawCaricamento(SpriteBatch spriteBatch)
        {

        }
    }
}
