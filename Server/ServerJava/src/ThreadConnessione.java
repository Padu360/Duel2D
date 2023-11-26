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

        ThreadSettaGiocatori t1 = new ThreadSettaGiocatori(server, giocatore1, 1);
        ThreadSettaGiocatori t2 = new ThreadSettaGiocatori(server, giocatore2, 2);

        t1.start();
        t2.start();

    }
}
