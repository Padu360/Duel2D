using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duel2D
{
    internal class animazioneSpecchio
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        private double count;
        private int ingradimento;
        private int tUpdate;


        public animazioneSpecchio(Texture2D texture, int rows, int columns, int ingrandimento, int tUpdate)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            ingradimento = ingrandimento;
            this.tUpdate = tUpdate;
        }

        public void Update(GameTime gameTime)
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

        public Rectangle Draw(SpriteBatch spriteBatch, Vector2 location, Rectangle destinationRectangle)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = currentFrame / Columns;
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            destinationRectangle.X = (int)location.X;
            destinationRectangle.Y = (int)location.Y;
            destinationRectangle.Width = width * ingradimento;
            destinationRectangle.Height = height * ingradimento;
            //destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width * ingradimento, height * ingradimento);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
            return destinationRectangle;
        }
    }
}
