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
        public int y = 640;
        public int hp;
        public string comando;

        public giocatore()
        {
            this.nome = "";
            this.x = 0;
            this.y = 640;
            this.hp = 100;
            this.comando = "muovi";
        }

        public giocatore(string nome, int nTexture)
        {
            this.nome = nome;
            this.nTexture = nTexture;
            this.x = 0;
            this.y = 640;
            this.hp = 100;
            this.comando = "muovi";
        }

        public giocatore(string nome, int x, int y, string comando)
        {
            this.nome = nome;
            this.nTexture = 0;
            this.x = x;
            this.y = y;
            this.hp = 100;
            this.comando = comando;
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

        public static giocatore toGiocatoreObj(string str)
        {
            try
            {
                string[] vet = str.Split(";");
                return new giocatore(vet[0], int.Parse(vet[1]), int.Parse(vet[2]), vet[3]);
            } catch (Exception e)
            {
                return new giocatore();
            }
        }

        public bool toGiocatore(string str)
        {
            string[] vet = str.Split(";");
            nome = vet[0];
            x = int.Parse(vet[1]);
            y = int.Parse(vet[2]);
            comando = vet[3];
            return true;
        }
    }
}
