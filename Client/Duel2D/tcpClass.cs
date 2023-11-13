using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Duel2D
{
    public class tcpClass
    {
        private static TcpClient client;
        private static NetworkStream stream;
        private static bool isRunning = true;
        private string ipAdress;
        private int port;
        private string msg = "ciao";

        public tcpClass(string ipAddress, int port)
        {
            this.ipAdress = ipAddress;
            this.port = port;
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

        public void riceviMessaggio()
        {
            while (isRunning)
            {
                try
                {
                    byte[] receivedBytes = new byte[1024];
                    int byteCount = stream.Read(receivedBytes, 0, receivedBytes.Length);

                    if (byteCount > 0)
                    {
                        string receivedMessage = Encoding.ASCII.GetString(receivedBytes, 0, byteCount);
                        Console.WriteLine("Server: " + receivedMessage);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e);
                    isRunning = false;
                }
            }
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

        /*
        static void Main()
        {
            string serverIP = "127.0.0.1"; // Indirizzo IP del server
            int serverPort = 8080; // Porta del server

            connettiServer();

            // Esempio di invio di un messaggio al server
            while (isRunning)
            {
                string message = Console.ReadLine();
                SendMessage(message);
            }

            stream.Close();
            client.Close();
        }
        */
    }
}
