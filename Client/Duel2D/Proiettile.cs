using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Duel2D
{
    internal class Proiettile   //classe che si occupa di creare un proiettile
    {
        public Rectangle pallottola;    //mi serve poi per vedere se colpisce un giocatore
        public int x { get; set; }
        public int y { get; set; }
        public string verso { get; set; } //verso del proiettile destra o sinistra

        public Proiettile()
        {
            x = 0; 
            y = 640;
        }

        public Proiettile(int x, int y, string verso)
        {
            this.x = x;
            this.y = y;
            pallottola = new Rectangle(this.x, this.y, 10, 5);
            this.verso = verso;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture) //disegno il proiettile
        {
            pallottola.X = x;
            pallottola.Y = y;
            if (verso.Equals("D"))
                spriteBatch.Draw(texture, pallottola, Color.White);
            if (verso.Equals("S"))
                spriteBatch.Draw(texture, pallottola, null, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
        }

        public bool controlla(Rectangle entita) //controllo se il nemico è stato colpito dal proiettile
        {
            if (entita.Contains(pallottola))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
