import java.util.Arrays;
import java.util.Scanner;

public class p11_EqualSums {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);

        int[] nums = Arrays
                .stream(scan.nextLine().split(" "))
                .mapToInt(Integer::parseInt)
                .toArray();

        if (!hasEqualSums(nums)) {
            System.out.println("no");
        }
    }

    private static boolean hasEqualSums(int[] nums) {

        for (int pos = 0; pos < nums.length; pos++) {
            if (leftSum(nums, pos) == rightSum(nums, pos)) {
                System.out.println(pos);
                return true;
            }
        }
        return false;
    }

    private static int rightSum(int[] nums, int pos) {
        return Arrays
                .stream(nums)
                .skip(pos + 1)
                .limit(nums.length - pos - 1)
                .sum();
    }

    private static int leftSum(int[] nums, int pos) {
        return Arrays
                .stream(nums)
                .limit(pos)
                .sum();
    }
}
