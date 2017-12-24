import java.util.Scanner;

public class p17_ChangeToUppercase {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);

        String text = scan.nextLine();

        while (text.contains("<upcase>")){
            String upperStr = text.substring(text.indexOf("<upcase>") + 8,
                    text.indexOf("</upcase>"));
            text = text.replaceFirst("<upcase>" + upperStr + "</upcase>", upperStr.toUpperCase());
        }

        System.out.println(text);
    }
}
