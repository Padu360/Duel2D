using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Duel2D
{
    internal class Proiettili   //classe che si occupa di gestire tutti i proiettili
    {
        public Texture2D tProiettile;   //texture proiettile
        public List<Proiettile> lista;
        public double countSparo; //counter velocità proiettili

        public Proiettili() {
            lista = new List<Proiettile>();
        }

        public void carica(Microsoft.Xna.Framework.Content.ContentManager content) //carico texture
        {
            tProiettile = content.Load<Texture2D>("proiettile");
        }

        public void push(int x, int y, string verso)    //aggiungo proiettile alla lista
        {
            if (verso.Equals("D"))
                lista.Add(new Proiettile(x + 85, y + 30, verso));
            if (verso.Equals("S"))
                lista.Add(new Proiettile(x + 35, y + 30, verso));
            
        }

        public void Update(GameTime gameTime, Rectangle entita)
        {
            for (int i = 0; i < lista.Count; i++)       //controllo se colpisco qualche giocatore
            {
                if (lista.ElementAt(i).controlla(entita))
                {
                    lista.RemoveAt(i);  //in caso rimuovo il proiettile
                } 
            }

            countSparo += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (countSparo >= 50)       //muovo il proiettile aggiornando le cordinate
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    if(lista.ElementAt(i).verso.Equals("D"))
                        lista.ElementAt(i).x += 8;
                    if(lista.ElementAt(i).verso.Equals("S"))
                        lista.ElementAt(i).x -= 8;
                }
                countSparo = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)   //disegno tutti i proiettili
        {
            for(int i = 0; i < lista.Count; i++)
            {
                lista.ElementAt(i).Draw(spriteBatch, tProiettile);
            }
        }
    }
}
