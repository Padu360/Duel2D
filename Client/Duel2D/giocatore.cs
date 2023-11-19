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
        public string comando;

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

        public void update()
        {

        }

        public string toCsvAll()      //invio le informazioni base del giocatore, quindi quando effettuo la prima connessione al server
        {
            return nome + ";" + nTexture + ";";
        }

        public string toCsv()         //invio solo le informazioni di update durante la partita come x, y e comando
        {
            return nome + ";" + x + ";" + y + ";" + comando;
        }

        public static giocatore toGiocatore(string str)
        {
            string[] vet = str.Split(";");
            return new giocatore(vet[0], int.Parse(vet[1]));
        }
    }
}
