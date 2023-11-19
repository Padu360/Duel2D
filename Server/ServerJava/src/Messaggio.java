public class Messaggio {

    public String messaggio;
    public String nome;
    public String x;
    public String y;
    public String comando;

    public Messaggio(String messaggio) {
        this.messaggio = messaggio;
    }

    public void Splitta() {
        String[] str = messaggio.split(";");
        nome = str[0];
        x = str[1];
        y = str[2];
        comando = str[3];
    }

}
