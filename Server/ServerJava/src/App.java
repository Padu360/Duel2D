public class App {
    public static void main(String[] args) throws Exception {
        TCPServerMOD server1 = new TCPServerMOD();
        Giocatore giocatore1 = new Giocatore();
        Giocatore giocatore2 = new Giocatore();
        // TCPServerMOD server2 = new TCPServerMOD();
        ThreadConnessione threadConnessione1 = new ThreadConnessione(server1, giocatore1);
        ThreadConnessione threadConnessione2 = new ThreadConnessione(server1, giocatore2);
        threadConnessione1.start();
        threadConnessione2.join();
        threadConnessione2.start();
        threadConnessione2.join();

    }
}
