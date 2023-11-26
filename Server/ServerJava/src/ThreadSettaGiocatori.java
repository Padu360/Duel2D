import java.io.IOException;

public class ThreadSettaGiocatori extends Thread {
    private TCPServerMOD server;
    private Giocatore giocatore;
    private int nClient;

    public ThreadSettaGiocatori(TCPServerMOD Server, Giocatore giocatore, int nClient) {
        this.server = Server;
        this.giocatore = giocatore;
        this.nClient = nClient;

    }

    public void run() {
        // Setta parametri giocatore ricevuti dal client
        Messaggio msg;
        try {
            msg = new Messaggio(server.ricevi(nClient));
            System.out.println(msg.messaggio);
        } catch (IOException e) {

            e.printStackTrace();
            return;
        }
        msg.Splitta();
        this.giocatore = new Giocatore(msg.nome, Integer.parseInt(msg.x),
                Integer.parseInt(msg.y));

        if (nClient == 1) {
            server.invia(msg.toCsv(), 2);
        } else {
            server.invia(msg.toCsv(), 1);
        }

    }

}
