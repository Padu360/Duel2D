using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Duel2D
{
    internal class animazione //classe che si occupa di gestire le animazioni
    {
        public Texture2D Texture { get; set; }  //qui viene salvata la texture della animazione (es s1corre.png ecc)
        public int Rows { get; set; }           //per disegnare una animazione si usa una foto non una gif o simili, questa foto viene divisa in colonne, e se necessario righe
        public int Columns { get; set; }
        private int currentFrame;               //indica il frame che si sta disegnando 
        private int totalFrames;                //si ottengono da quanti frame è composta l'immagine, nel nostro caso tutte hanno 4 o più frame
        private double count;                   
        private int ingradimento;               //di quanto voglio ingrandire la texture
        private int tUpdate;                    //variabile per scegliere la velocità della animazione


        public animazione(Texture2D texture, int rows, int columns, int ingrandimento, int tUpdate)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            ingradimento = ingrandimento;
            this.tUpdate = tUpdate;
        }

        public void Update(GameTime gameTime)       //con l'update aggiorno il frame da disegnare
        {
            count += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (count >= tUpdate)
            {
                currentFrame++;
                count = 0;
                if (currentFrame == totalFrames)
                    currentFrame = 0;
            }
        }

        public Rectangle Draw(SpriteBatch spriteBatch, Vector2 location, Rectangle destinationRectangle)    //per disegnare il frame
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = currentFrame / Columns;
            int column = currentFrame % Columns;
            //bisogna calcolare che pezzo di immagine prendere in base al frame da disegnare

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            destinationRectangle.X = (int)location.X;
            destinationRectangle.Y = (int)location.Y;
            destinationRectangle.Width = width * ingradimento;
            destinationRectangle.Height = height * ingradimento;

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);  //disegno
            return destinationRectangle; //faccio return per poi capire se l'entità è stata colpita da un proiettile
        }
    }
}
