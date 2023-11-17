using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Duel2D
{
    internal class menu
    {
        public inputNome inputNome { get; set; }
        public giocatore giocatore { get; set; }
        public animazione soldato1 { get; set; }
        public animazione soldato2 { get; set; }

        public Texture2D tso1 { get; set; }
        public Texture2D tso2 { get; set; }
        public Texture2D btnNormale { get; set; }
        public Texture2D btnOn { get; set; }
        public Texture2D cLeft { get; set; }
        public Texture2D cRight { get; set; }

        public int statoCasella = 0;
        public int selezioneSoldato = 0;
        public double count;
        public bool gioca = false;
    
        public menu() { 
            inputNome = new inputNome();
        }

        public void carica(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            inputNome.carica(content);
            btnNormale = content.Load<Texture2D>("giocaBase");
            btnOn = content.Load<Texture2D>("giocaClick");
            cLeft = content.Load<Texture2D>("cLeft");
            cRight = content.Load<Texture2D>("cRight");

            tso1 = content.Load<Texture2D>("soldato5");
            soldato1 = new animazione(tso1, 1, 4, 10, 150);
            tso2 = content.Load<Texture2D>("soldato6");
            soldato2 = new animazione(tso2, 1, 4, 10, 150);
        }

        public void Update(GameTime gameTime)
        {
            inputNome.Update();
            MouseState mouseState = Mouse.GetState();
            int x = mouseState.X;
            int y = mouseState.Y;

            //---------------pulsante gioca--------------------------------
            if ((x >= 505 && x <= 695) && (y >= 630 && y <= 700))
            {
                if (statoCasella != 2)
                    statoCasella = 1;
                if (mouseState.LeftButton == ButtonState.Pressed && inputNome.isSetName())
                {
                    statoCasella = 2;
                    gioca = true;
                }
            }
            else
            {
                if (statoCasella != 2 && statoCasella != 3)
                    statoCasella = 0;
            }
            //--------------------------------------------------------------

            //--------------pulsanti seleziona skin-------------------------
            count += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (count >= 140)
            {
                if ((x >= 420 && x <= 500) && (y >= 450 && y <= 530))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (selezioneSoldato == 0)
                            selezioneSoldato = 1;
                        else
                            selezioneSoldato = 0;
                    }
                }
                if ((x >= 690 && x <= 770) && (y >= 450 && y <= 530))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (selezioneSoldato == 1)
                            selezioneSoldato = 0;
                        else
                            selezioneSoldato = 1;
                    }
                }

                count = 0;
            }
            //--------------------------------------------------------------

            //-------------aggiorno animazioni soldati----------------------
            soldato1.Update(gameTime);
            soldato2.Update(gameTime);
            //--------------------------------------------------------------
        }

        public void DrawMenu(SpriteBatch spriteBatch)
        {
            inputNome.Draw(spriteBatch);


            //draw tipi giocatori
            spriteBatch.Draw(cLeft, new Rectangle(420, 450, 80, 80), Color.White);
            spriteBatch.Draw(cRight, new Rectangle(690, 450, 80, 80), Color.White);

            if (selezioneSoldato == 0)
                soldato1.Draw(spriteBatch, new Vector2(445, 295));
            if (selezioneSoldato == 1)
                soldato2.Draw(spriteBatch, new Vector2(445, 295));


            //draw pulsante gioca
            if (statoCasella == 0)
            {
                spriteBatch.Draw(btnNormale, new Rectangle(505, 630, 190, 70), Color.White);
            }
            else if (statoCasella == 1 || statoCasella == 2)
            {
                spriteBatch.Draw(btnOn, new Rectangle(505, 630, 190, 70), Color.White);
            }
        }

        public giocatore getGiocatore()
        {
            return new giocatore(inputNome.getNome(), selezioneSoldato);
        }

        public bool isGioca()
        {
            return gioca;
        }
    }
}
