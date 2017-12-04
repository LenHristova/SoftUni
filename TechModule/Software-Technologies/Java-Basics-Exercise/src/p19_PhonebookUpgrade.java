import java.util.Scanner;
import java.util.TreeMap;

public class p19_PhonebookUpgrade {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);

        TreeMap<String, String> phoneBook = new TreeMap<>();

        while (true) {
            String input = scan.nextLine();
            if (input.equals("END")) {
                break;
            }

            String[] commandParams = input.split(" ");
            String command = commandParams[0];

            switch (command) {
                case "A": {
                    String name = commandParams[1];
                    String phone = commandParams[2];
                    phoneBook.put(name, phone);
                    break;
                }
                case "S": {
                    String name = commandParams[1];
                    if (phoneBook.containsKey(name)) {
                        System.out.printf("%s -> %s%n", name, phoneBook.get(name));
                    } else {
                        System.out.printf("Contact %s does not exist.%n", name);
                    }
                    break;
                }
                default:
                    for (String n : phoneBook.keySet()) {
                        System.out.printf("%s -> %s%n", n, phoneBook.get(n));
                    }
                    break;
            }
        }
    }
}

