import java.util.Scanner;

public class p13_ReverseString {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);

        String word = scan.nextLine();

        StringBuilder reversedWord = new StringBuilder();
        for (int i = word.length()-1; i >= 0; i--) {
            reversedWord.append(word.charAt(i));
        }

        System.out.println(reversedWord);
    }
}
