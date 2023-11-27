using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Net.Mime;


namespace Duel2D
{
    public class tcpClass
    {
        public Texture2D serveron { get; set; }     //nella classe tcp sono presenti delle texture perchè nella schermata menu/home è presente un icona in alto a sinistra
        public Texture2D serveroff { get; set; }    //per vedere se si è connessi al server

        private static TcpClient client;
        private static NetworkStream stream;
        private string ipAdress;
        private int port;
        public bool connection = false;            
        private bool t = false;
        private bool nuovo = false;
        private bool ricevendo = false;

        public string msgRicevuto { get; set; }

        public tcpClass(string ipAddress, int port)
        {
            this.ipAdress = ipAddress;
            this.port = port;
        }

        public void Update()    //per connettersi al server all'inzio tramite l'uso dei thread (per non stoppare il menu)
        {
            if (!connection && !t)
            {
                Thread connectionThread = new Thread(ConnessioneServer);
                connectionThread.Start();
                t = true;
            }
        }

        private void ConnessioneServer()    //richiamo questa funzione nel thread che chiama un'altra funzione che restituisce un bool
        {
            connection = this.connettiServer();
            if (!connection) { t = false; } //in si è stabilita la connessione non c'è più bisogno che io cerchi di connettermi al server
        }

        public void carica(Microsoft.Xna.Framework.Content.ContentManager content)      //carico texture status server
        {
            serveron = content.Load<Texture2D>("serverOn");
            serveroff = content.Load<Texture2D>("serverOff");
        }

        public void DrawStatus(SpriteBatch spriteBatch)     //disegno lo status 
        {
            if (connection)
                spriteBatch.Draw(serveron, new Rectangle(7, 1, 93, 38), Color.White);
            else
                spriteBatch.Draw(serveroff, new Rectangle(7, 1, 93, 38), Color.White);
        }

        public bool connettiServer()        //funzione per collegare il client al server la prima volta
        {
            try
            {
                client = new TcpClient(ipAdress, port);
                stream = client.GetStream();

                return true;
            }
            catch (SocketException e)
            {
                return false;
            }
            catch (IOException ex)
            {
                return false;
            }
            catch (ObjectDisposedException ex)
            {
                return false;
            }
        }

        public void ricevi()        //da usare per i thread
        {
            try
            {
                ricevendo = true;
                byte[] receivedBytes = new byte[1024];
                int byteCount = stream.Read(receivedBytes, 0, receivedBytes.Length);
                

                if (byteCount > 0)
                {
                    string receivedMessage = Encoding.ASCII.GetString(receivedBytes, 0, byteCount);
                    if (receivedMessage.Length > 6)
                    {
                        int terminatorIndex = receivedMessage.IndexOf("\r\n"); // Trova l'indice del terminatore

                        if (terminatorIndex >= 0)
                        {
                            msgRicevuto = receivedMessage.Substring(0, terminatorIndex); // Estrai la prima parte
                        }
                    } else
                    {
                        msgRicevuto = receivedMessage;
                    }
                    nuovo = true;
                }
                ricevendo = false;
                stream.Flush();
            }
            catch (Exception e)
            {
                 msgRicevuto = "null";
            }
        }

        public string getMessaggio() //siccome usando i thread non posso ritornare una stringa, salvo il messaggio ottenuto in una variabile della classe
        {
            if (nuovo == true)  //ottengo il messaggio solo se è arrivato veramente e non è un messaggio vecchio
            {
                nuovo = false;
                return msgRicevuto;
            }
            return "";
        }

        public void tRicevi()       //funzione che avvia il thread della ricevi
        {
            if (ricevendo == false)
            {
                Thread connectionThread = new Thread(ricevi);
                connectionThread.Start();
            }
        }

        public void invia(string msg)       //funzione che invia i dati
        {
            try
            {
                byte[] data = Encoding.ASCII.GetBytes(msg + "\r\n");
                stream.Write(data, 0, data.Length);
            }
            catch (Exception e)
            {
                
            }
        }

        public bool isRicevendo()       //per vedere se sta ricevendo, da usare nelle altre classi
        {
            return ricevendo;
        }
    }
}