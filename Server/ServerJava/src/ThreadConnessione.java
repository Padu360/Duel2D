import java.io.IOException;

public class ThreadConnessione extends Thread {
    private TCPServerMOD server;
    private Giocatore giocatore;

    public ThreadConnessione(TCPServerMOD Server, Giocatore giocatore) {
        this.server = Server;
        this.giocatore = giocatore;
    }

    public void run() {
        try {
            server.start(9999);
        } catch (IOException e) {

            e.printStackTrace();
        }

        Messaggio msg;
        try {
            msg = new Messaggio(server.ricevi());
        } catch (IOException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
            return;
        }
        giocatore = new Giocatore(msg.nome, Integer.parseInt(msg.x), Integer.parseInt(msg.y));
    }
}
