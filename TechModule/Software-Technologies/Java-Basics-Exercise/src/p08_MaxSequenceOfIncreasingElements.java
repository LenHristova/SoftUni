import java.util.ArrayList;
import java.util.Arrays;
import java.util.Scanner;

public class p08_MaxSequenceOfIncreasingElements {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);

        int[] nums = Arrays.stream(scan.nextLine().split(" "))
                .mapToInt(Integer::parseInt)
                .toArray();

        int counter = 1;
        int maxCount = 1;
        ArrayList maxSequence = new ArrayList();
        ArrayList temp = new ArrayList();
        temp.add(nums[0]);
        for (int i = 0; i < nums.length - 1; i++) {
            if (nums[i] < nums[i + 1]) {
                counter++;
                temp.add(nums[i + 1]);
                if (i == nums.length - 2 && counter > maxCount) {
                    maxCount = counter;
                    maxSequence.clear();
                    maxSequence.addAll(temp);
                }
            } else {
                if (counter > maxCount) {
                    maxCount = counter;
                    maxSequence.clear();
                    maxSequence.addAll(temp);
                }

                temp.clear();
                temp.add(nums[i + 1]);
                counter = 1;
            }
        }

        for (Object num : maxSequence) {
            System.out.print(num + " ");
        }
    }
}
