public class App {
    public static void main(String[] args) throws Exception {
        TCPServerMOD server = new TCPServerMOD();
        server.start(666);
        while (true) {
            System.out.println(server.ricevi());
        }
    }
}
