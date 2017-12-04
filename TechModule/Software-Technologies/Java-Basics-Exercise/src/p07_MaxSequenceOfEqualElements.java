import java.util.Arrays;
import java.util.Scanner;

public class p07_MaxSequenceOfEqualElements {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);

        int[] nums = Arrays.stream(scan.nextLine().split(" "))
                .mapToInt(Integer::parseInt)
                .toArray();

        int counter = 1;
        int maxCount = 1;
        int num = nums[0];
        for (int i = 0; i < nums.length - 1; i++) {
            if (nums[i] == nums[i + 1]) {
                counter++;
                if (i == nums.length - 2 && counter > maxCount) {
                    maxCount = counter;
                    num = nums[i];
                }
            } else {
                if (counter > maxCount) {
                    maxCount = counter;
                    num = nums[i];
                }

                counter = 1;
            }
        }

        String[] maxSequence = new String[maxCount];
        Arrays.fill(maxSequence, "" + num);
        System.out.println(String.join(" ", maxSequence));
    }
}
