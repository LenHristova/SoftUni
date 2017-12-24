import java.util.Scanner;

public class p05_SymmetricNumbers {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);

        int num = scan.nextInt();
        for (int currNum = 1; currNum <= num; currNum++) {
            if (isSymmetric(currNum)) {
                System.out.print(currNum + " ");
            }
        }
    }

    private static boolean isSymmetric(int num) {
        String numStr = "" + num;

        for (int i = 0; i < numStr.length() / 2; i++) {
            if (numStr.charAt(i) != numStr.charAt(numStr.length() - 1 - i)){
                return false;
            }
        }

        return true;
    }
}