import java.util.Scanner;

public class p02_BooleanVariable {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);

        Boolean isTrue = Boolean.valueOf(scan.nextLine());
        System.out.println(isTrue ? "Yes" : "No");
    }
}
