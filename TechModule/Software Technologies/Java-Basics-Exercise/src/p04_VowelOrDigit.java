import java.util.Scanner;

public class p04_VowelOrDigit {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);


        String symbol = scan.nextLine();
        if (isVowel(symbol)) {
            System.out.println("vowel");
        } else if (isDigit(symbol)) {
            System.out.println("digit");
        } else {
            System.out.println("other");
        }
    }

    private static boolean isDigit(String symbol) {
        try {
            Integer.parseInt(symbol);
            return true;
        } catch (NumberFormatException e) {
            return false;
        }
    }

    private static boolean isVowel(String symbol) {
        String[] vowels = {"a", "o", "u", "e", "i", "y"};
        for (String vowel : vowels) {
            if (symbol.equals(vowel)) {
                return true;
            }
        }
        return false;
    }
}
