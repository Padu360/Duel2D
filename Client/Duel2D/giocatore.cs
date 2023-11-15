using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duel2D
{
    internal class giocatore
    {
        public string nome;
        public int nTexture;
        public int x;
        public int y;

        public giocatore()
        {
            this.nome = "";
            this.x = 0;
            this.y = 0;
        }

        public giocatore(string nome, int nTexture)
        {
            this.nome = nome;
            this.nTexture= nTexture;
            this.x = 0;
            this.y = 0;
        }

       
    }
}
