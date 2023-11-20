using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Duel2D
{
    internal class inputNome
    {
        KeyboardState letteraPrecedente;
        public Texture2D sNome { get; set; }
        public Texture2D sNomeVuota { get; set; }
        public SpriteFont fAll { get; set; }
        private String nome;
        private int statoCasella = 0;

        public inputNome()
        {
            nome = "";
        }

        public void carica(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            sNome = content.Load<Texture2D>("nome2");
            sNomeVuota = content.Load<Texture2D>("nomeVuoto");
            fAll = content.Load<SpriteFont>("fontNome");
        }

        public void Update()
        {
            KeyboardState state = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            int x = mouseState.X;
            int y = mouseState.Y;

            if ((x >= 480 && x <= 720) && (y >= 120 && y <= 205))
            {
                if(statoCasella != 2 && statoCasella != 3) 
                    statoCasella = 1;
                if(mouseState.LeftButton == ButtonState.Pressed)
                    statoCasella = 2;
            }
            else
            {
                if (statoCasella != 2 && statoCasella != 3)
                    statoCasella = 0;
                if (mouseState.LeftButton == ButtonState.Pressed && isSetName())
                    statoCasella = 3;
                if (mouseState.LeftButton == ButtonState.Pressed && !isSetName())
                    statoCasella = 0;
            }

            if(statoCasella == 2)
            {
                Keys[] lettere = state.GetPressedKeys();
                foreach (Keys key in lettere)
                {
                    if (state.IsKeyDown(key) && !letteraPrecedente.IsKeyDown(key))
                    {
                        if (key >= Keys.A && key <= Keys.Z && nome.Length < 8)
                            nome += key.ToString();
                        else if (key == Keys.Back && nome.Length > 0)
                            nome = nome.Substring(0, nome.Length - 1);
                    }
                }

                letteraPrecedente = state;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(statoCasella == 0)
            {
                spriteBatch.Draw(sNome, new Rectangle(480, 120, 240, 85), Color.White * 0.75f);
            } else if(statoCasella == 1)
            {
                spriteBatch.Draw(sNome, new Rectangle(480, 120, 240, 85), Color.White * 0.84f);
            } else if(statoCasella == 2)
            {
                spriteBatch.Draw(sNomeVuota, new Rectangle(480, 120, 240, 85), Color.White * 0.96f);
                spriteBatch.DrawString(fAll, nome, new Vector2(505, 142), Color.Black);
            } else if(statoCasella == 3)
            {
                spriteBatch.Draw(sNomeVuota, new Rectangle(480, 120, 240, 85), Color.White * 0.84f);
                spriteBatch.DrawString(fAll, nome, new Vector2(505, 142), Color.Black * 0.9f);
            }
        }

        public string getNome()
        {
            return nome;
        }

        public bool isSetName()
        {
            if (nome != "")
                return true;
            return false;
        }
    }
}
