import java.io.IOException;

public class ThreadConnessione extends Thread {
    private TCPServerMOD server;

    public ThreadConnessione(TCPServerMOD Server) {
        this.server = Server;
    }

    public void run() {
        try {
            server.start(666);
        } catch (IOException e) {

            e.printStackTrace();
        }

    }
}
