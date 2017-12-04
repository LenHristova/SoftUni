import java.util.Arrays;
import java.util.HashMap;
import java.util.Scanner;

public class p09_MostFrequentNumber {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);

        int[] nums = Arrays.stream(scan.nextLine().split(" "))
                .mapToInt(Integer::parseInt)
                .toArray();

        HashMap<Integer, Integer> numsFrequency = new HashMap<>();
        for (int num : nums) {
            if (!numsFrequency.containsKey(num)) {
                numsFrequency.put(num, 1);
            } else {
                numsFrequency.put(num, numsFrequency.get(num) + 1);
            }
        }

        nums = Arrays.stream(nums).distinct().toArray();
        int mostFrequentNum = nums[0];
        int maxFrequency = numsFrequency.get(mostFrequentNum);
        for (int i = 1; i < nums.length; i++) {
            if (numsFrequency.get(nums[i]) > maxFrequency){
                maxFrequency = numsFrequency.get(nums[i]);
                mostFrequentNum = nums[i];
            }
        }

        System.out.println(mostFrequentNum);
    }
}
