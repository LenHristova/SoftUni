import java.util.Scanner;

public class p04_SumNIntegers {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);

        int n = scan.nextInt();

        int sum = 0;
        for (int i = 0; i < n; i++) {
            sum += scan.nextInt();
        }

        System.out.printf("Sum = %d", sum);
    }
}
