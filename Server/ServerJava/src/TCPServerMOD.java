import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.ServerSocket;
import java.net.Socket;

public class TCPServerMOD {
    public ServerSocket serverSocket;
    public Socket clientSocket1;
    public PrintWriter out1;
    public BufferedReader in1;
    public Socket clientSocket2;
    public PrintWriter out2;
    public BufferedReader in2;
    public String nomeClient1;
    public String nomeClient2;

    public TCPServerMOD() {
    }

    /**
     * Attende richiesta di connessione da 2 client e manda riposta positiva
     * se si connette un client
     * 
     * @param port porta del server
     * @throws IOException
     */
    public void start(int port) throws IOException {
        serverSocket = new ServerSocket(port);

        // Client 1
        clientSocket1 = serverSocket.accept();
        out1 = new PrintWriter(clientSocket1.getOutputStream(), true);
        in1 = new BufferedReader(new InputStreamReader(clientSocket1.getInputStream()));
        out1.println("Connessione stabilita");

        // Client 2
        clientSocket2 = serverSocket.accept();
        out2 = new PrintWriter(clientSocket1.getOutputStream(), true);
        in2 = new BufferedReader(new InputStreamReader(clientSocket1.getInputStream()));
        out2.println("Connessione stabilita");
    }

    public void stop() throws IOException {
        in1.close();
        out1.close();
        clientSocket1.close();
        in2.close();
        out2.close();
        clientSocket2.close();
        serverSocket.close();
    }

    public String ricevi(int nClient) throws IOException {
        String str = "";

        // while (true) {
        if (nClient == 1) {
            str = in1.readLine();
            System.out.println(str);
            return str;
        } else if (nClient == 2) {
            str = in2.readLine();
            System.out.println(str);
            return str;
        }

        return "ERRORE ID CLIENT";
        // }
    }

    public void invia(String msg, int nClient) {
        if (nClient == 1) {
            out1.println(msg);
        } else if (nClient == 2) {
            out2.println(msg);
        }
    }
}
