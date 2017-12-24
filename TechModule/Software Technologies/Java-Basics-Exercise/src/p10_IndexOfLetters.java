import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.Scanner;

public class p10_IndexOfLetters {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);
        ArrayList<Character> alphabet = getLetters();

        String word = scan.nextLine();
        for (int i = 0; i < word.length(); i++) {
            char currLetter = word.charAt(i);
            int pos = alphabet.indexOf(currLetter);
            System.out.println(currLetter + " -> " + pos);
        }
    }

    private static ArrayList<Character> getLetters() {
        ArrayList<Character> alphabet = new ArrayList<>();
        for (char i = 'a'; i <= 'z'; i++) {
            alphabet.add(i);
        }

        return alphabet;
    }
}
