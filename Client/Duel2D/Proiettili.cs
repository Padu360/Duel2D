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
    internal class Proiettili
    {
        public Texture2D tProiettile;
        public List<Proiettile> lista;
        public double countSparo;

        public Proiettili() {
            lista = new List<Proiettile>();
        }

        public void carica(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            tProiettile = content.Load<Texture2D>("proiettile");
        }

        public void push(int x, int y, string verso)
        {
            if (verso.Equals("D"))
                lista.Add(new Proiettile(x + 85, y + 30, verso));
            if (verso.Equals("S"))
                lista.Add(new Proiettile(x + 35, y + 30, verso));
            
        }

        public void Update(GameTime gameTime, Rectangle entita)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista.ElementAt(i).controlla(entita))
                {
                    lista.RemoveAt(i);
                } 
            }

            countSparo += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (countSparo >= 80)
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    if(lista.ElementAt(i).verso.Equals("D"))
                        lista.ElementAt(i).x += 11;
                    if(lista.ElementAt(i).verso.Equals("S"))
                        lista.ElementAt(i).x -= 11;
                }
                countSparo = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < lista.Count; i++)
            {
                lista.ElementAt(i).Draw(spriteBatch, tProiettile);
            }
        }
    }
}
