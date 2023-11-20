import java.io.IOException;

public class ThreadPartita extends Thread {
    private TCPServerMOD connPrincipale;
    private TCPServerMOD connSecondaria;
    private Giocatore giocatorePrincipale;
    private Giocatore giocatoreSecondario;

    public ThreadPartita(TCPServerMOD connPrincipale, TCPServerMOD connSecondaria, Giocatore giocatorePrincipale,
            Giocatore giocatoreSecondario) {
        this.connPrincipale = connPrincipale;
        this.connSecondaria = connSecondaria;
        this.giocatorePrincipale = giocatorePrincipale;
        this.giocatoreSecondario = giocatoreSecondario;
    }

    public void run() {
        boolean inCorso = true;
        String msg = "";
        do {
            try {
                msg = connPrincipale.ricevi();

                if (msg.contains("colpito")) {
                    int vita = giocatorePrincipale.colpito();
                    if (vita > 0) {
                        connPrincipale.invia(giocatorePrincipale.nome + ";" + vita);
                        connSecondaria.invia(giocatorePrincipale.nome + ";" + vita);
                    } else {
                        connPrincipale.invia(giocatorePrincipale.nome + ";" + "sconfitta");
                        connSecondaria.invia(giocatorePrincipale.nome + ";" + "vittoria");
                    }

                } else if (msg.contains("muovi")) {
                    Messaggio messaggio = new Messaggio(msg);
                    messaggio.Splitta();
                    giocatorePrincipale.muovi(Integer.parseInt(messaggio.x), Integer.parseInt(messaggio.y));
                    connPrincipale.invia(messaggio.toCsv());
                    connSecondaria.invia(messaggio.toCsv());
                } else if (msg.contains("salta")) {
                    connPrincipale.invia(giocatorePrincipale.nome + ";" + "salta");
                    connSecondaria.invia(giocatorePrincipale.nome + ";" + "salta");
                } else if (msg.equals("fine")) {
                    connPrincipale.invia("fine");
                    connSecondaria.invia("fine");
                    inCorso = false;
                } else if (msg.equals("rivincita")) {
                    giocatorePrincipale.vita = 100;
                    giocatoreSecondario.vita = 100;
                }

            } catch (IOException e) {
                e.printStackTrace();
                System.out.println("Errore nella ricezione del messaggio!");
                return;
            }

        } while (inCorso);
    }

}
