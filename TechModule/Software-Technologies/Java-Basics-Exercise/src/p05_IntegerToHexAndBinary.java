import java.util.Scanner;

public class p05_IntegerToHexAndBinary {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);

        int num = Integer.parseInt(scan.nextLine());
        System.out.println((Integer.toHexString(num)).toUpperCase());
        System.out.println(Integer.toBinaryString(num));
    }
}
