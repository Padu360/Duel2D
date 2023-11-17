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
            Byte[] data;
            try
            {
                client = new TcpClient(ipAdress, port);
                
                data = System.Text.Encoding.ASCII.GetBytes(msg);
                stream = client.GetStream();

                data = new Byte[256];                           //RICEVI
                String responseData = String.Empty;
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);
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


            //stream.Write(data, 0, data.Length);           INVIA
            //Console.WriteLine("Sent: {0}", msg);


            

            /*
            Thread receiveThread = new Thread(riceviMessaggio);
            receiveThread.Start();


            // Invia un messaggio al server
            Byte[] data2 = System.Text.Encoding.ASCII.GetBytes(msg);
            stream.Write(data2, 0, data2.Length);         
            Console.WriteLine("Sent: {0}", msg);
            */








            /*
                * stream.Close();
            client.Close();
                * 
                * 
            try
            {
                client = new TcpClient(ipAdress, port);
                stream = client.GetStream();
                Console.WriteLine("Connected to server.");

                // Avvia il thread per la ricezione dei messaggi in modo asincrono
                Thread receiveThread = new Thread(riceviMessaggio);
                receiveThread.Start();

                // Invia un messaggio al server
                inviaMessaggio("Hello, server!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e);
            }
            */
            return true;
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
                    return receivedMessage;
                }
                return null;
            }
            catch (SocketException e)
            {
                return null;
            }
            

            /* da implementare thread
            if (!ricevendo)
            {
                Thread connectionThread = new Thread(riceviMessaggio);
                connectionThread.Start();
            }
            return null;
            */
        }

        public void invia(string msg)
        {
            try
            {
                byte[] data = Encoding.ASCII.GetBytes(msg);
                stream.Write(data, 0, data.Length);
            } catch (SocketException e)
            {

            }
        }


        private string riceviMessaggio()
        {
            try
            {
                byte[] receivedBytes = new byte[1024];
                int byteCount = stream.Read(receivedBytes, 0, receivedBytes.Length);

                if (byteCount > 0)
                {
                    string receivedMessage = Encoding.ASCII.GetString(receivedBytes, 0, byteCount);
                    return receivedMessage;
                }
                return null;
            }
            catch (SocketException e)
            {
                return null;
            }
        }

        public bool isRicevendo()
        {
            return ricevendo;
        }

















        public void inviaMessaggio(string message)
        {
            try
            {
                byte[] data = Encoding.ASCII.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e);
            }
        }
    }
}