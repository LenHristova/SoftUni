import java.util.Scanner;

public class p01_VariableInHexadecimalFormat {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);

        System.out.println(Integer.parseInt(scan.nextLine(), 16));
    }
}