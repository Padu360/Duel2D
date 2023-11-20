import java.io.IOException;

public class ThreadConnessione extends Thread {
    private TCPServerMOD server;
    private Giocatore giocatore;
    private int port;

    public ThreadConnessione(TCPServerMOD Server, Giocatore giocatore, int port) {
        this.server = Server;
        this.giocatore = giocatore;
        this.port = port;
    }

    public void run() {
        try {
            server.start(port);
        } catch (IOException e) {

            e.printStackTrace();
        }

        Messaggio msg;
        try {
            msg = new Messaggio(server.ricevi());
            System.out.println(msg.messaggio);
        } catch (IOException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
            return;
        }
        this.giocatore = new Giocatore(msg.nome, Integer.parseInt(msg.x),
                Integer.parseInt(msg.y));

    }
}
