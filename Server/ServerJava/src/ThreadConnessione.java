import java.io.IOException;

public class ThreadConnessione extends Thread {
    private TCPServerMOD server;
    private Giocatore giocatore1;
    private Giocatore giocatore2;
    private int port;

    public ThreadConnessione(TCPServerMOD Server, Giocatore giocatore1, Giocatore giocatore2, int port) {
        this.server = Server;
        this.giocatore1 = giocatore1;
        this.giocatore2 = giocatore2;
        this.port = port;
    }

    public void run() {
        try {
            server.start(port);
        } catch (IOException e) {

            e.printStackTrace();
        }

        // Setta parametri giocatore ricevuti dal client 1
        Messaggio msg;
        try {
            msg = new Messaggio(server.ricevi(1));
            // System.out.println(msg.messaggio);
        } catch (IOException e) {

            e.printStackTrace();
            return;
        }
        msg.Splitta();
        this.giocatore1 = new Giocatore(msg.nome, Integer.parseInt(msg.x),
                Integer.parseInt(msg.y));

        server.invia(msg.toCsv(), 2);

        // Setta parametri giocatore ricevuti dal client 2
        try {
            msg = new Messaggio(server.ricevi(2));
            // System.out.println(msg.messaggio);
        } catch (IOException e) {

            e.printStackTrace();
            return;
        }
        msg.Splitta();
        this.giocatore2 = new Giocatore(msg.nome, Integer.parseInt(msg.x),
                Integer.parseInt(msg.y));

        server.invia(msg.toCsv(), 1);

    }
}
