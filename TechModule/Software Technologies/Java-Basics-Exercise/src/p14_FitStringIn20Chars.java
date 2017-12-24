import java.util.Scanner;

public class p14_FitStringIn20Chars {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);

        String phrase = scan.nextLine();
        if (phrase.length() >= 20){
            System.out.println(phrase.substring(0, 20));
        }else {
            System.out.println(padRight(phrase));
        }
    }

    private static String padRight(String phrase) {
        StringBuilder phraseBuilder = new StringBuilder(phrase);
        for (int j = 0; j < 20 - phrase.length(); j++) {
            phraseBuilder.append('*');
        }
        return phraseBuilder.toString();
    }
}
