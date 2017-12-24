import java.util.HashMap;
import java.util.Scanner;

public class p18_Phonebook {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);

        HashMap<String, String> phoneBook = new HashMap<>();

        while (true) {
            String input = scan.nextLine();
            if (input.equals("END")) {
                break;
            }

            String[] commandParams = input.split(" ");
            String command = commandParams[0];
            String name = commandParams[1];

            if (command.equals("A")) {
                String phone = commandParams[2];
                phoneBook.put(name, phone);
            } else {
                if (phoneBook.containsKey(name)) {
                    System.out.printf("%s -> %s%n", name, phoneBook.get(name));
                } else {
                    System.out.printf("Contact %s does not exist.%n", name);
                }
            }
        }
    }
}
