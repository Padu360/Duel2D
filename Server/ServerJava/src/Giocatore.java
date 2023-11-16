
public class Giocatore {
    public String nome;
    public int nTexture;
    public int x;
    public int y;
    final int MAX_WIDTH = 100;
    final int MIN_WIDTH = 10;

    public Giocatore() {
    }

    public Giocatore(String nome, int nTexture, int x, int y) {
        this.nome = nome;
        this.nTexture = nTexture;
        this.x = x;
        this.y = y;
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

}
