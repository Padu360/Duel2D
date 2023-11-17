public class App {
    public static void main(String[] args) throws Exception {
        TCPServerMOD server1 = new TCPServerMOD();
        TCPServerMOD server2 = new TCPServerMOD();
        ThreadConnessione threadConnessione1 = new ThreadConnessione(server1);
        ThreadConnessione threadConnessione2 = new ThreadConnessione(server2);
        threadConnessione1.start();
        threadConnessione2.start();

    }
}
