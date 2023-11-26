using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public int incrementoSalto = 0;
        public double countSalto;
        public bool salto = false;
        public int versoSalto = 0;
        public double countInvio;
        public string uInvio = "";


        public partita(tcpClass tmp)
        {
            giocatore = new giocatore();
            giocatoreTmp = new giocatore();
            avversario = new giocatore();
            aGiocatore = new Animazioni();
            aAvversario = new Animazioni();
            gestProiettili = new Proiettili();
            clientTcp = tmp;
        }

        public void Carica(Microsoft.Xna.Framework.Content.ContentManager content)  //carico tutte le texture e animazioni necessarie
        {
            sGioco = content.Load<Texture2D>("sGioco");
            gestProiettili.carica(content);

            aGiocatore.carica(content);
            aAvversario.carica(content);
        }

        public void Update(GameTime gameTime)       //update che si occupa dei movimenti, ricevi e invia messaggi, setta posizioni ecc
        {
            KeyboardState keyboardState = Keyboard.GetState();

            //gestori animazioni dei proiettili, giocatore e avversario
            gestProiettili.Update(gameTime, aAvversario.entita);
            aGiocatore.Update(gameTime);
            aAvversario.Update(gameTime);

            //parte che si occupa del muovimento su asse x con tasti D e S del giocatore
            countMovimento += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (countMovimento >= 80)
            {
                if (aGiocatore.azione != 4 && aGiocatore.azione != 5 && aGiocatore.azione != 6 && aGiocatore.azione != 7)
                {
                    if (keyboardState.IsKeyDown(Keys.A))
                    {
                        giocatore.x = giocatore.x - 2;
                        aGiocatore.azione = 3;
                        aGiocatore.verso = "S";
                    }
                    else if (keyboardState.IsKeyDown(Keys.D))
                    {
                        giocatore.x = giocatore.x + 2;
                        aGiocatore.azione = 2;
                        aGiocatore.verso = "D";
                    }
                }
            }

            //parte che si occupa del salto
            if (salto == true)       //probabilmente c'è un modo migliore per gestire l'incremento e decremento del salto
            {
                countSalto += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (countSalto >= 20)
                {
                    if (versoSalto == 0)
                    {
                        if (incrementoSalto < 22)
                        {
                            giocatore.y = giocatore.y - 4;
                            incrementoSalto++;
                        }
                        else
                        {
                            incrementoSalto = 0;
                            versoSalto = 1;
                        }
                    }
                    if (versoSalto == 1)
                    {
                        if (incrementoSalto < 22)
                        {
                            giocatore.y = giocatore.y + 4;
                            incrementoSalto++;
                        }
                        else
                        {
                            incrementoSalto = 0;
                            versoSalto = 0;
                            salto = false;
                            if (aGiocatore.verso.Equals("D"))
                                aGiocatore.azione = 2;
                            if (aGiocatore.verso.Equals("S"))
                                aGiocatore.azione = 3;
                        }
                    }
                    countSalto = 0;
                }
            }


            if (salto != true)
            {
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    if (aGiocatore.verso.Equals("D"))
                        aGiocatore.azione = 6;
                    if (aGiocatore.verso.Equals("S"))
                        aGiocatore.azione = 7;
                    salto = true;
                }
            }
            //fine parte che si occupa del salto


            //parte che si occupa di gestire la lista dei proiettili del giocatore
            countSparo += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (countSparo >= 250)      //aggiungo i proiettili alla lista
            {
                if (aGiocatore.azione == 4 || aGiocatore.azione == 5)
                {
                    gestProiettili.push(giocatore.x, giocatore.y, aGiocatore.verso);
                }
                countSparo = 0;
            }


            giocatore.comando = aGiocatore.azione.ToString();
            string msgInvio = giocatore.toCsv();
            if (msgInvio.Equals(uInvio))        //questa si occupa di gestire quando il giocatore si trova in idle
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
            if (!uInvio.Equals(msgInvio))       //invio il messaggio, se è differente da quello precedente, in quanto se non cambia nulla è inutile intasare la banda e il server
            {
                clientTcp.invia(msgInvio);
                countInvio = 0;
            }
            uInvio = msgInvio;

            clientTcp.tRicevi();        //parte che si occupa di ricevere i messaggi e smistarli
            string muovimenti = clientTcp.getMessaggio();
            string[] vet;
            if (!muovimenti.Equals(""))
            {
                Debug.WriteLine(muovimenti);
                vet = muovimenti.Split(";");
                /*
                if (vet[0] == giocatore.nome)
                {
                    giocatore.toGiocatore(muovimenti);
                }
                */
                if (vet[0] == avversario.nome)
                {
                    avversario.toGiocatore(muovimenti);
                    Debug.WriteLine(muovimenti);
                    aAvversario.azione = Int32.Parse(avversario.comando);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)       //parte che si occupa di disegnare il tutto
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