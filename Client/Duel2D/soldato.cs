﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Duel2D
{
    internal class soldato
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        private double count;
        


        public soldato(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
        }

        public void Update(GameTime gameTime)
        {
            count += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (count >= 120)
            {
                currentFrame++;
                count = 0;
                if (currentFrame == totalFrames)
                    currentFrame = 0;
            }
        }

        public void DrawIdle(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = currentFrame / Columns;
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width * 10, height * 10);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
