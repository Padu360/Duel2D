public class App {
    public static void main(String[] args) throws Exception {

        while (true) {
            TCPServerMOD conn1 = new TCPServerMOD();
            TCPServerMOD conn2 = new TCPServerMOD();
            Giocatore giocatore1 = new Giocatore();
            Giocatore giocatore2 = new Giocatore();

            ThreadConnessione threadConnessione1 = new ThreadConnessione(conn1,
                    giocatore1, 9999);
            // ThreadConnessione threadConnessione2 = new ThreadConnessione(conn2,
            // giocatore2, 10000);
            threadConnessione1.start();
            threadConnessione1.join();
            // threadConnessione2.start();
            // threadConnessione2.join();

            ThreadPartita threadPartita1 = new ThreadPartita(conn1, conn2, giocatore1, giocatore2);
            // ThreadPartita threadPartita2 = new ThreadPartita(conn2, conn1, giocatore2,
            // giocatore1);
            threadPartita1.start();
            threadPartita1.join();
            // threadPartita2.start();
            //

        }

    }
}
