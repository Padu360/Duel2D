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
using System.Diagnostics;

namespace Duel2D
{
    internal class Screen
    {
        public int schermata = 0;                       //per decidere in che schermata essere
        //asset necessari per le schermate e font
        public Texture2D sIniziale { get; set; }
        public Texture2D sMenu { get; set; }
        public Texture2D sCaricamento { get; set; }
        public Texture2D ruotaCaricamento { get; set; }
        public animazione caricamento { get; set; }
        public SpriteFont fAll { get; set; }

        //oggetti che mi servono 
        public tcpClass clientTcp { get; set; }
        public menu menu { get; set; }
        public partita game { get; set; }

        //oggetti temporanei del giocatore e avversario
        public giocatore giocatore { get; set; }
        public giocatore avversario { get; set; }

        public string rInvio = "";
        private float opacitaTI = 0.1f;
        private bool sensoOpacita = true;
        public bool inviato = false;

        public double counter = 0;


        public Screen()
        {
            clientTcp = new tcpClass("localhost", 9999);  //creo il client tcp, server vero su cui provarlo: 89.40.142.55 (ora off)
            menu = new menu();                            //creo il gestore del menu
            giocatore = new giocatore();
            avversario = new giocatore();
            game = new partita(clientTcp);
        }

        public void carica(Microsoft.Xna.Framework.Content.ContentManager content)  //carico tutte le texture necessarie
        {
            sIniziale = content.Load<Texture2D>("sIniziale");
            sMenu = content.Load<Texture2D>("sMenu");
            sCaricamento = content.Load<Texture2D>("sCaricamento");
            ruotaCaricamento = content.Load<Texture2D>("caricamento");
            fAll = content.Load<SpriteFont>("fAll");
            clientTcp.carica(content);
            menu.carica(content);
            caricamento = new animazione(ruotaCaricamento, 1, 5, 2, 220);
            game.Carica(content);
        }

        public void unload() //funzione per rimuovere le texture che non uso
        {

        }

        public void updateStart(GameTime gameTime)  //update per gestire lo start
        {

        }

        public void updateMenu(GameTime gameTime)   //update per gestire il menu, in questo caso:
        {
            clientTcp.Update();                 //connetto al server
            menu.Update(gameTime);              //update del gestore menu
            giocatore = menu.getGiocatore();    //creo giocatore
                                                // if (menu.isGioca())                 //aspetto gli altri giocatori se ho cliccato su gioca 
                                                //  schermata = 2;                  //mi sposto su schermata caricamento



            
            if (menu.isGioca())
            {
                if (inviato == false)
                {
                    clientTcp.invia(giocatore.toCsv());         //invio le informazioni al server del giocatore in modo che possa creare una partita
                    inviato = true;
                }

                clientTcp.tRicevi();                            //ricevo messaggi dal server
                string amsg = clientTcp.getMessaggio();
                if (amsg != rInvio)                             //verifico che il messaggio ricevuto non sia come altri già ricevuti
                {
                    if(amsg != "" || amsg != null)
                        avversario = giocatore.toGiocatoreObj(amsg);
                    Debug.WriteLine(avversario.nome);
                    if (avversario.nome != "" && giocatore.nome != avversario.nome)                  //se l'avversario ha ancora il nome di default vuol dire che il server non ha inviato niente e che non devo avviare la partita
                    {
                        game.giocatore = giocatore;
                        game.avversario = avversario;
                        schermata = 3;
                    }
                }
                rInvio = amsg;
            }
        }

        public void updateCaricamento(GameTime gameTime)    //update per gestire la schermata di caricamento
        {
            caricamento.Update(gameTime);

            
            /*if (inviato == false)                           
            {
                clientTcp.invia(giocatore.toCsv());         //invio le informazioni al server del giocatore in modo che possa creare una partita
                inviato = true;
            }
            
            clientTcp.tRicevi();                            //ricevo messaggi dal server
            string amsg = clientTcp.getMessaggio();
            if (amsg != rInvio)                             //verifico che il messaggio ricevuto non sia come altri già ricevuti
            {
                avversario = giocatore.toGiocatoreObj(amsg);

                if (avversario.nome != "")                  //se l'avversario ha ancora il nome di default vuol dire che il server non ha inviato niente e che non devo avviare la partita
                {
                    game.giocatore = giocatore;
                    game.avversario = avversario;
                    schermata = 3;
                }
            }
            rInvio = amsg;*/
        }

        public void updateGioco(GameTime gameTime)      //update per gestire il gioco
        {
            game.Update(gameTime);  //richiamo la funzione update della classe partita che si occuperà del resto
        }

        public void animazioneTesto()   //per fare la scritta figa che pulsa a inizio gioco 
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

        public void DrawStart(SpriteBatch spriteBatch)      //disegno lo start
        {
            animazioneTesto();  //richiamo per disegnare la scritta in modo che pulsi

            spriteBatch.Begin();
            spriteBatch.Draw(sIniziale, new Rectangle(0, 0, 1200, 800), Color.White);
            spriteBatch.DrawString(fAll, "premi un qualsiasi pulsante per continuare", new Vector2(250, 620), Color.White * opacitaTI);
            spriteBatch.End();
        }

        public void DrawMenu(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            spriteBatch.Draw(sMenu, new Rectangle(0, 0, 1200, 800), Color.White);   //disegno sfondo menu
            clientTcp.DrawStatus(spriteBatch);  //disegno lo status del server, on o off
            menu.DrawMenu(spriteBatch);     //disegno bottone gioca, skin ecc

            spriteBatch.End();
        }

        public void DrawCaricamento(SpriteBatch spriteBatch)  //per disegnare la schermata di caricamento
        {
            animazioneTesto();
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            spriteBatch.Draw(sCaricamento, new Rectangle(0, 0, 1200, 800), Color.White * 0.16f); //sfondo
            caricamento.Draw(spriteBatch, new Vector2(552, 320), new Rectangle());               //animazione
            spriteBatch.DrawString(fAll, "in attesa di giocatori..", new Vector2(420, 490), Color.White * opacitaTI);

            spriteBatch.End();
        }

        public void DrawGioco(SpriteBatch spriteBatch)  //per disegnare il gameplay
        {
            game.Draw(spriteBatch); //ha una sua funzione dedicata però
        }

        internal int getSchermata()  //funzione che utilizzo nel file game1
        {
            return schermata;
        }

        internal int setSchermata(int x)  //funzione che utilizzo nel file game1
        {
            schermata = x;
            return schermata;
        }
    }
}
