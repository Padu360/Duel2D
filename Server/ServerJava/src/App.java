import java.net.ServerSocket;
import java.net.Socket;

public class App {
    public static void main(String[] args) throws Exception {
        TCPServerMOD server = new TCPServerMOD();
        server.start(666);
    }
}
