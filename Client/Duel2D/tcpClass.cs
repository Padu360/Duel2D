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

namespace Duel2D
{
    public class tcpClass
    {
        public Texture2D serveron { get; set; }
        public Texture2D serveroff { get; set; }

        private static TcpClient client;
        private static NetworkStream stream;
        private static bool isRunning = true;
        private string ipAdress;
        private int port;
        private string msg = "ciao";
        private bool connection = false;
        private bool t = false;
        private bool inviando = false;
        private bool ricevendo = false;

        public string msgRicevuto { get; set; }

        public tcpClass(string ipAddress, int port)
        {
            this.ipAdress = ipAddress;
            this.port = port;
        }

        public void Update()
        {
            if (!connection && !t)
            {
                Thread connectionThread = new Thread(ConnessioneServer);
                connectionThread.Start();
                t = true;
            }
        }

        private void ConnessioneServer()
        {
            connection = this.connettiServer();
            if (!connection) { t = false; }
        }

        public void carica(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            serveron = content.Load<Texture2D>("serverOn");
            serveroff = content.Load<Texture2D>("serverOff");
        }

        public void DrawStatus(SpriteBatch spriteBatch)
        {
            if (connection)
                spriteBatch.Draw(serveron, new Rectangle(7, 1, 93, 38), Color.White);
            else
                spriteBatch.Draw(serveroff, new Rectangle(7, 1, 93, 38), Color.White);
        }

        public bool connettiServer()
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

        public string ricevi()
        {
            try
            {
                byte[] receivedBytes = new byte[1024];
                int byteCount = stream.Read(receivedBytes, 0, receivedBytes.Length);

                if (byteCount > 0)
                {
                    string receivedMessage = Encoding.ASCII.GetString(receivedBytes, 0, byteCount);
                    msgRicevuto = receivedMessage;
                    return msgRicevuto;
                }
            }
            catch (SocketException e)
            {
                 return msgRicevuto = "";
            }
            return "";
        }

        public void invia(string msg)
        {
            try
            {
                byte[] data = Encoding.ASCII.GetBytes(msg + "\r\n");
                stream.Write(data, 0, data.Length);
            }
            catch (SocketException e)
            {

            }
        }

        public bool isRicevendo()
        {
            return ricevendo;
        }
    }
}