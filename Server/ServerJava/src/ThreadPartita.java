import java.io.IOException;

public class ThreadPartita extends Thread {
    private TCPServerMOD connessione;
    private Giocatore giocatorePrincipale;
    private Giocatore giocatoreSecondario;
    private int nClientPrincipale;
    private int nClientSecondario;

    public ThreadPartita(TCPServerMOD connessione, int nClientPrincipale, int nClientSecondario,
            Giocatore giocatorePrincipale,
            Giocatore giocatoreSecondario) {
        this.connessione = connessione;
        this.nClientPrincipale = nClientPrincipale;
        this.giocatorePrincipale = giocatorePrincipale;
        this.giocatoreSecondario = giocatoreSecondario;
    }

    public void run() {
        boolean inCorso = true;
        String msg = "";
        do {
            try {
                msg = connessione.ricevi(nClientPrincipale);
                System.out.println(msg);
                if (msg.contains("colpito")) {
                    int vita = giocatorePrincipale.colpito();
                    if (vita > 0) {
                        connessione.invia(giocatorePrincipale.nome + ";" + vita, nClientPrincipale);
                        connessione.invia(giocatorePrincipale.nome + ";" + vita, nClientSecondario);
                    } else {
                        connessione.invia(giocatorePrincipale.nome + ";" + "sconfitta", nClientPrincipale);
                        connessione.invia(giocatorePrincipale.nome + ";" + "vittoria", nClientSecondario);
                    }

                } else if (msg.contains("muovi")) {
                    Messaggio messaggio = new Messaggio(msg);
                    messaggio.Splitta();
                    giocatorePrincipale.muovi(Integer.parseInt(messaggio.x), Integer.parseInt(messaggio.y));
                    connessione.invia(messaggio.toCsv(), nClientPrincipale);
                    connessione.invia(messaggio.toCsv(), nClientSecondario);
                } else if (msg.contains("salta")) {
                    connessione.invia(giocatorePrincipale.nome + ";" + "salta", nClientPrincipale);
                    connessione.invia(giocatorePrincipale.nome + ";" + "salta", nClientSecondario);
                } else if (msg.contains("fine")) {
                    connessione.invia("fine", nClientPrincipale);
                    connessione.invia("fine", nClientSecondario);
                    inCorso = false;
                } else if (msg.contains("rivincita")) {
                    giocatorePrincipale.vita = 100;
                    giocatoreSecondario.vita = 100;
                } else if (msg.contains("sparaADestra")) {
                    connessione.invia(giocatorePrincipale.nome + ";" + "sparaADestra", nClientPrincipale);
                    connessione.invia(giocatorePrincipale.nome + ";" + "sparaADestra", nClientSecondario);
                } else if (msg.contains("sparaASinistra")) {
                    connessione.invia(giocatorePrincipale.nome + ";" + "sparaASinistra", nClientPrincipale);
                    connessione.invia(giocatorePrincipale.nome + ";" + "sparaASinistra", nClientSecondario);
                }

            } catch (IOException e) {
                e.printStackTrace();
                System.out.println("Errore nella ricezione del messaggio!");
                return;
            }

        } while (inCorso);
    }

}
