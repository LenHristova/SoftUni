import java.util.Scanner;

public class p15_CensorEmailAddress {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);
        String email = scan.nextLine();
        String text = scan.nextLine();

        String[] emailParams = email.split("@");
        String censoredEmail = emailParams[0].replaceAll(".", "*") +
                "@" + emailParams[1];
        System.out.println(text.replaceAll(email, censoredEmail));
    }
}
