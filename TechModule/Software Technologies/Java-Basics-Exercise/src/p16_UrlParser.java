import java.util.Scanner;

public class p16_UrlParser {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);

        String url = scan.nextLine();
        String protocol = "";
        String server = "";
        String resource = "";

        if (url.contains("://")) {
            String[] urlParams = url.split("://");
            protocol = urlParams[0];
            server = urlParams[1];
        }else {
            server = url;
        }

        if (server.contains("/")) {
            resource = server.substring(server.indexOf("/") + 1);
            server = server.substring(0, server.indexOf("/"));

        }

        System.out.printf(
                "[protocol] = \"%s\"%n" +
                        "[server] = \"%s\"%n" +
                        "[resource] = \"%s\"%n", protocol, server, resource);
    }
}
