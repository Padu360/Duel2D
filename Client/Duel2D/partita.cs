﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duel2D
{
    internal class partita
    {
        public Texture2D sGioco { get; set; }
        public Texture2D corre { get; set; }
        public Texture2D spara { get; set; }
        

        public animazione omino;
        public animazione omino2;
        public animazione sparo;
        tcpClass clientTcp;

        public giocatore giocatore { get; set; }
        public giocatore giocatoreTmp { get; set; }
        public giocatore avversario { get; set; }
        public double countSparo;
        public double countMovimento;
        public double countSparoAnimazione;
        public double countInvio;
        public string uInvio = "";
        public int azione = 0;
        public int x = 0;
        public int y = 640;
        private bool ric = true;


        public partita(tcpClass tmp)
        {
            giocatore = new giocatore("andre", 1);
            giocatoreTmp = new giocatore("andre", 1);
            avversario = new giocatore("nemico", 2);
            clientTcp = tmp;
        }

        public void Carica(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            sGioco = content.Load<Texture2D>("sGioco");
            corre = content.Load<Texture2D>("s1corre");
            spara = content.Load<Texture2D>("s1spara");
            omino = new animazione(corre, 1, 4, 3, 170);
            omino2 = new animazione(corre, 1, 4, 3, 170);
            sparo = new animazione(spara, 1, 4, 3, 170);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            int mx = mouseState.X;
            int my = mouseState.Y;

            if (azione == 0)
                omino.Update(gameTime);
            if (azione == 1)
                sparo.Update(gameTime);

            //if (ric)
            //{
            countMovimento += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (countMovimento >= 80)
            {
                if (azione != 1)
                {
                    if (keyboardState.IsKeyDown(Keys.A))
                    {
                        giocatoreTmp.x = giocatoreTmp.x - 2;
                        giocatoreTmp.comando = "muovi";
                        azione = 0;
                    }
                    else if (keyboardState.IsKeyDown(Keys.D))
                    {
                        giocatoreTmp.x = giocatoreTmp.x + 2;
                        giocatoreTmp.comando = "muovi";
                        azione = 0;
                    }
                }
            }


                countSparo += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (countSparo >= 80)
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                        azione = 1;

                    countSparo = 0;
                }


                countSparoAnimazione += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (countSparoAnimazione >= 680 && azione == 1)
                {
                    azione = 0;
                    countSparoAnimazione = 0;

                }

            ric = false;
            string msgInvio = giocatoreTmp.toCsv();
            countInvio += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (!uInvio.Equals(msgInvio))
            {
                clientTcp.invia(msgInvio);
                countInvio = 0;
            }
            uInvio = msgInvio;

            clientTcp.tRicevi();
            string muovimenti = clientTcp.getMessaggio();
            string[] vet;
            if (!muovimenti.Equals("null"))
            {
                vet = muovimenti.Split(";");
                if (vet[0] == giocatore.nome)
                {
                    ric = giocatore.toGiocatore(muovimenti);
                }
                if (vet[0] == avversario.nome)
                {
                    ric = avversario.toGiocatore(muovimenti);
                }
            }
            
 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(sGioco, new Rectangle(0, 0, 1200, 800), Color.White);

            if (azione == 0)
                omino.Draw(spriteBatch, new Vector2(giocatore.x, giocatore.y));
            if (azione == 1)
                sparo.Draw(spriteBatch, new Vector2(giocatore.x, giocatore.y));

            if (avversario.comando.Equals("muovi"))
                omino2.Draw(spriteBatch, new Vector2(avversario.x, avversario.y));
            if (avversario.comando.Equals("spara"))
                sparo.Draw(spriteBatch, new Vector2(avversario.x, avversario.y));


            spriteBatch.End();
        }
    }
}
