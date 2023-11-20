import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.ServerSocket;
import java.net.Socket;

public class TCPServerMOD {
    public ServerSocket serverSocket;
    public Socket clientSocket;
    public PrintWriter out;
    public BufferedReader in;
    public String nomeClient1;
    public String nomeClient2;

    public TCPServerMOD() {
    }

    /**
     * Attende richiesta di connessione e invia messaggio
     * se la connessione è stabilita
     * 
     * @param port porta del server
     * @throws IOException
     */
    public void start(int port) throws IOException {
        serverSocket = new ServerSocket(port);
        clientSocket = serverSocket.accept();
        out = new PrintWriter(clientSocket.getOutputStream(), true);
        in = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));

        // out.println("Connessione stabilita");
    }

    public void stop() throws IOException {
        in.close();
        out.close();
        clientSocket.close();
        serverSocket.close();
    }

    public String ricevi() throws IOException {
        String str;

        str = in.readLine();
        System.out.println(str);

        return str;
    }

    public void invia(String msg) {
        out.println(msg);
    }
}
