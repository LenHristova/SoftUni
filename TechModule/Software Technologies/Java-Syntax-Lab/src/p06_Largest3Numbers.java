import java.util.Arrays;
import java.util.Scanner;

public class p06_Largest3Numbers {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);

        int[] nums = Arrays.stream(
                scan.nextLine()
                        .split(" "))
                .mapToInt(Integer::parseInt)
                .sorted()
                .toArray();

        for (int num : Arrays
                        .stream(reverseArr(nums))
                        .limit(3)
                        .toArray()) {
            System.out.println(num);
        }

    }

    private static int[] reverseArr(int[] arr) {

        for (int i = 0; i < arr.length / 2; i++) {
            int temp = arr[i];
            arr[i] = arr[arr.length - 1 - i];
            arr[arr.length - 1 - i] = temp;
        }

        return arr;
    }
}
