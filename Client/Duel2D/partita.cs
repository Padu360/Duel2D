using Microsoft.Xna.Framework;
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

        public Animazioni aGiocatore;
        public Animazioni aAvversario;

        tcpClass clientTcp;

        public Proiettili gestProiettili;
        public giocatore giocatore { get; set; }
        public giocatore giocatoreTmp { get; set; }
        public giocatore avversario { get; set; }
        public double countSparo;
        public double countMovimento;
        public bool salto = false;
        public double countInvio;
        public string uInvio = "";


        public partita(tcpClass tmp)
        {
            giocatore = new giocatore("andre", 1);
            giocatoreTmp = new giocatore("andre", 1);
            avversario = new giocatore("nemico", 2);
            aGiocatore = new Animazioni();
            aAvversario = new Animazioni();
            gestProiettili = new Proiettili();
            clientTcp = tmp;
        }

        public void Carica(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            sGioco = content.Load<Texture2D>("sGioco");
            gestProiettili.carica(content);

            aGiocatore.carica(content);
            aAvversario.carica(content);
        }

        public void Update(GameTime gameTime)
        {
            avversario.x = 500;
            KeyboardState keyboardState = Keyboard.GetState();

            gestProiettili.Update(gameTime, aAvversario.entita);
            aGiocatore.Update(gameTime);
            aAvversario.Update(gameTime);


            countMovimento += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (countMovimento >= 80)
            {
                if (aGiocatore.azione != 4 && aGiocatore.azione != 5 && aGiocatore.azione != 6 && aGiocatore.azione != 7)
                {
                    if (keyboardState.IsKeyDown(Keys.A))
                    {
                        giocatoreTmp.x = giocatoreTmp.x - 2;
                        aGiocatore.azione = 3;
                        aGiocatore.verso = "S";
                        giocatoreTmp.comando = "muovi";
                    }
                    else if (keyboardState.IsKeyDown(Keys.D))
                    {
                        giocatoreTmp.x = giocatoreTmp.x + 2;
                        aGiocatore.azione = 2;
                        aGiocatore.verso = "D";
                        giocatoreTmp.comando = "muovi";
                    }                    
                }
            }

            if(salto != true)
            {
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    if (aGiocatore.verso.Equals("D"))
                        aGiocatore.azione = 6;
                    if (aGiocatore.verso.Equals("S"))
                        aGiocatore.azione = 7;
                    salto = true;
                    /*
                    giocatoreTmp.x = giocatoreTmp.x + 2;
                    aGiocatore.azione = 2;
                    aGiocatore.verso = "D";
                    giocatoreTmp.comando = "muovi";
                    */
                }
            }



            countSparo += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (countSparo >= 250)      //aggiungo i proiettili alla lista
            {
                if (aGiocatore.azione == 4 || aGiocatore.azione == 5)
                {
                    gestProiettili.push(giocatoreTmp.x, giocatoreTmp.y, aGiocatore.verso);
                }
                countSparo = 0;
            }
           


            string msgInvio = giocatoreTmp.toCsv();
            if (msgInvio.Equals(uInvio))
            {
                if (aGiocatore.azione != 4 && aGiocatore.azione != 5 && aGiocatore.azione != 6 && aGiocatore.azione != 7)
                {
                    if (aGiocatore.verso.Equals("D"))
                        aGiocatore.azione = 0;
                    if (aGiocatore.verso.Equals("S"))
                        aGiocatore.azione = 1;
                }
            }


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
                    giocatore.toGiocatore(muovimenti);
                }
                if (vet[0] == avversario.nome)
                {
                    avversario.toGiocatore(muovimenti);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            spriteBatch.Draw(sGioco, new Rectangle(0, 0, 1200, 800), Color.White);

            aGiocatore.Draw(spriteBatch, giocatore.x, giocatore.y);
            aAvversario.Draw(spriteBatch, avversario.x, avversario.y);

            gestProiettili.Draw(spriteBatch); 

            spriteBatch.End();
        }
    }
}
