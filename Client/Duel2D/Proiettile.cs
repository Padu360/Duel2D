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
    internal class Proiettile
    {
        public Rectangle pallottola;
        public int x { get; set; }
        public int y { get; set; }
        public string verso;

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

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            pallottola.X = x;
            pallottola.Y = y;
            spriteBatch.Draw(texture, pallottola, Color.White);
        }
    }
}
