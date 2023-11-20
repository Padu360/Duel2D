
public class Giocatore {
    public String nome;
    public int nTexture;
    public int x;
    public int y;
    public int vita;
    final int MAX_WIDTH = 100;
    final int MIN_WIDTH = 10;
    final int MAX_VITA = 100;
    final int DANNI_COLPO = 10;

    public Giocatore() {
    }

    public Giocatore(String nome, int x, int y) {
        this.nome = nome;
        // this.nTexture = nTexture;
        this.x = x;
        this.y = y;
        this.vita = MAX_VITA;
    }

    public String muovi(int x, char direzione) {
        String ris = "";
        switch (direzione) {
            case 'L':
                if (this.x - x >= MIN_WIDTH) {
                    this.x -= x;
                }
                break;

            case 'R':
                if (this.x + x <= MAX_WIDTH) {
                    this.x += x;
                }
                break;

            default:
                break;
        }

        ris = nome + ";" + x + "";

        return ris;
    }

    public String muovi(int x, int y) {
        String ris = "";

        ris = nome + ";" + x + ";" + y;

        return ris;
    }

    public int colpito() {
        vita -= DANNI_COLPO;
        if (vita <= 0) {
            vita = 0;
        }
        return vita;
    }

}
