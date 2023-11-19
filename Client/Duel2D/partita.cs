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
    internal class partita
    {
        public Texture2D sGioco { get; set; }
        public Texture2D corre { get; set; }
        public Texture2D spara { get; set; }

        public animazione test;
        public animazione sparo;

        public giocatore giocatore { get; set; }
        public giocatore avversario { get; set; }
        public double countSparo;
        public double countSparoAnimazione;
        public int azione = 0;
        private bool oldStateM;


        public partita()
        {
            giocatore = new giocatore("andre", 1);
            avversario = new giocatore("nemico", 2);
        }

        public void Carica(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            sGioco = content.Load<Texture2D>("sGioco");
            corre = content.Load<Texture2D>("s1corre");
            spara = content.Load<Texture2D>("s1spara");
            test = new animazione(corre, 1, 4, 3, 170);
            sparo = new animazione(spara, 1, 4, 3, 170);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            int mx = mouseState.X;
            int my = mouseState.Y;

            if (azione == 0)
                test.Update(gameTime);
            if (azione == 1)
                sparo.Update(gameTime);


            if (azione != 1)
            {
                if (keyboardState.IsKeyDown(Keys.A))
                {
                    giocatore.x = giocatore.x - 2;
                    azione = 0;
                }
                else if (keyboardState.IsKeyDown(Keys.D))
                {
                    giocatore.x = giocatore.x + 2;
                    azione = 0;
                }
            }



            countSparo += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (countSparo >= 80)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    azione = 1;

                countSparo = 0;
            }


            countSparoAnimazione += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (countSparoAnimazione >= 680 && azione == 1)
            {
                azione = 0;
                countSparoAnimazione = 0;

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(sGioco, new Rectangle(0, 0, 1200, 800), Color.White);

            if (azione == 0)
                test.Draw(spriteBatch, new Vector2(giocatore.x, giocatore.y));
            if (azione == 1)
                sparo.Draw(spriteBatch, new Vector2(giocatore.x, giocatore.y));


            spriteBatch.End();
        }
    }
}
