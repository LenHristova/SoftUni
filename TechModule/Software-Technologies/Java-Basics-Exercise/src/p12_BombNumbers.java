import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.Scanner;

public class p12_BombNumbers {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);

        ArrayList<String> nums = new ArrayList<>();
        Collections.addAll(nums, scan.nextLine().split(" "));

        String[] bombParams = scan.nextLine().split(" ");
        String bombNumber = bombParams[0];
        int bombPower = Integer.parseInt(bombParams[1]);

        while (nums.contains(bombNumber)) {
            int index = nums.indexOf(bombNumber);
            int leftPower = Math.min(bombPower, index);
            int rightPower = Math.min(bombPower, nums.size() - index -1);

            int startIndex = index - leftPower;
            int count = leftPower + rightPower + 1;
            nums = removeRange(nums, startIndex, count);
        }
        int sum = Arrays
                .stream(nums.toArray())
                .mapToInt(x -> Integer.parseInt(x.toString()))
                .sum();

        System.out.println(sum);
    }

    private static ArrayList<String> removeRange(
            ArrayList<String> nums, int startIndex, int count) {
        for (int i = 0; i < count; i++) {
            nums.remove(startIndex);
        }

        return nums;
    }

}
