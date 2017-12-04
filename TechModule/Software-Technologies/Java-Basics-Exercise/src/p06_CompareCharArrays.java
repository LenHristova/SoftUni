import java.util.Scanner;

public class p06_CompareCharArrays {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);

        String str1 = scan.nextLine().replaceAll(" ", "");
        String str2 = scan.nextLine().replaceAll(" ", "");

        int smallerArrLength = Math.min(str1.length(), str2.length());

        boolean isOrdered = false;
        for (int i = 0; i < smallerArrLength; i++) {
            if (str1.charAt(i) < str2.charAt(i)) {
                System.out.println(str1);
                System.out.println(str2);
                isOrdered = true;
                break;
            } else if (str1.charAt(i) > str2.charAt(i)) {
                System.out.println(str2);
                System.out.println(str1);
                isOrdered = true;
                break;
            }
        }

        if (!isOrdered) {
            if (str1.length() < str2.length()) {
                System.out.println(str1);
                System.out.println(str2);
            } else {
                System.out.println(str2);
                System.out.println(str1);
            }
        }
    }
}