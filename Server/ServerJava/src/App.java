public class App {
    public static void main(String[] args) throws Exception {

        while (true) {
            TCPServerMOD connessione = new TCPServerMOD();
            // TCPServerMOD conn2 = new TCPServerMOD();
            Giocatore giocatore1 = new Giocatore();
            Giocatore giocatore2 = new Giocatore();

            ThreadConnessione threadConnessione1 = new ThreadConnessione(connessione,
                    giocatore1, giocatore2, 9999);
            // ThreadConnessione threadConnessione2 = new ThreadConnessione(conn2,
            // giocatore2, 10000);
            threadConnessione1.start();
            threadConnessione1.join();
            // threadConnessione2.start();
            // threadConnessione2.join();

            ThreadPartita threadPartita1 = new ThreadPartita(connessione, 1, 2, giocatore1, giocatore2);
            ThreadPartita threadPartita2 = new ThreadPartita(connessione, 2, 1, giocatore2, giocatore1);
            threadPartita1.start();
            threadPartita2.start();
            threadPartita1.join();
            threadPartita2.join();

        }

        // System.out.println("Test");
    }
}
