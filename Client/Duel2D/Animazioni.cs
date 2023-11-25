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
    internal class Animazioni //classe che si occupa di gestire le animazioni di tutto il gioco
    {
        public Texture2D idlet { get; set; }    //creo oggetto texture nel nostro caso 4
        public Texture2D corret { get; set; }
        public Texture2D sparat { get; set; }
        public Texture2D saltat { get; set; }

        public animazione idle;                 //creo 4 oggetti animazione e 4 oggetti con l'animazione specchiata
        public animazione corre;
        public animazione spara;
        public animazione salta;
        public animazioneSpecchio idles;
        public animazioneSpecchio corres;
        public animazioneSpecchio sparas;
        public animazioneSpecchio saltas;

        public Rectangle entita { get; set; }   //entita del giocatore per poi verificare se è stato colpito da un proiettile

        public int azione { get; set; }         //che azione sta facendo il giocatore, corre, salta, spara ecc
        public double countSparoAnimazione;     
        public double countSparo;
        public int nTexture;                
        public string verso { get; set; }       //verso se si trova a destra o sinistra il giocatore

        public Animazioni() {
            entita = new Rectangle();
            verso = "";
        }

        public void carica(Microsoft.Xna.Framework.Content.ContentManager content)  //carico tutte le texture e animazioni
        {
            if (nTexture == 0)
            {
                idlet = content.Load<Texture2D>("s1idle");
                corret = content.Load<Texture2D>("s1corre");
                sparat = content.Load<Texture2D>("s1spara");
                saltat = content.Load<Texture2D>("s1salta");
            } else if(nTexture == 1)
            {
                idlet = content.Load<Texture2D>("s2idle");
                corret = content.Load<Texture2D>("s2corre");
                sparat = content.Load<Texture2D>("s2spara");
                saltat = content.Load<Texture2D>("s2salta");
            }

            idle = new animazione(idlet, 1, 4, 3, 170);
            corre = new animazione(corret, 1, 4, 3, 170);
            spara = new animazione(sparat, 1, 4, 3, 170);
            salta = new animazione(saltat, 1, 4, 3, 170);
            idles = new animazioneSpecchio(idlet, 1, 4, 3, 170);
            corres = new animazioneSpecchio(corret, 1, 4, 3, 170);
            sparas = new animazioneSpecchio(sparat, 1, 4, 3, 170);
            saltas = new animazioneSpecchio(saltat, 1, 4, 3, 170);
        }

        public void Update(GameTime gameTime)       //controllo se ha sparato e aggiorno i frame delle animazioni interessate a quello che sta svolgendo
        {
            MouseState mouseState = Mouse.GetState();
            int mx = mouseState.X;
            int my = mouseState.Y;

            if (azione == 0)
                idle.Update(gameTime);
            if (azione == 1)
                idles.Update(gameTime);

            if (azione == 2)
                corre.Update(gameTime);
            if (azione == 3)
                corres.Update(gameTime);

            if (azione == 4)
                spara.Update(gameTime);
            if (azione == 5)
                sparas.Update(gameTime);

            if (azione == 6)
                salta.Update(gameTime);
            if (azione == 7)
                saltas.Update(gameTime);


            countSparoAnimazione += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (countSparoAnimazione >= 680 && (azione == 4 || azione == 5))
            {
                if (verso.Equals("S"))
                    azione = 3;
                if (verso.Equals("D"))
                    azione = 2;
                countSparoAnimazione = 0;
            }

            
            countSparo += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (countSparo >= 250)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (verso.Equals("D"))
                        azione = 4;
                    if (verso.Equals("S"))
                        azione = 5;
                }

                countSparo = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, int x, int y) //in base alla azione che svolge disegno la texture
        {
            if (azione == 0)
                entita = idle.Draw(spriteBatch, new Vector2(x, y), entita);
            if (azione == 1)
                entita = idles.Draw(spriteBatch, new Vector2(x, y), entita);

            if (azione == 2)
                entita = corre.Draw(spriteBatch, new Vector2(x, y), entita);
            if (azione == 3)
                entita = corres.Draw(spriteBatch, new Vector2(x, y), entita);

            if (azione == 4)
                entita = spara.Draw(spriteBatch, new Vector2(x, y), entita);
            if (azione == 5)
                entita = sparas.Draw(spriteBatch, new Vector2(x, y), entita);

            if (azione == 6)
                entita = salta.Draw(spriteBatch, new Vector2(x, y), entita);
            if (azione == 7)
                entita = saltas.Draw(spriteBatch, new Vector2(x, y), entita);
        }
    }
}
