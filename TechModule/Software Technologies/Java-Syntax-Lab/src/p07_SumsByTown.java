import java.util.Arrays;
import java.util.Scanner;
import java.util.TreeMap;

public class p07_SumsByTown {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);

        int n = Integer.parseInt(scan.nextLine());

        TreeMap<String, Double> townsIncomes = new TreeMap<>();
        for (int i = 0; i < n; i++) {
            String[] tokens = Arrays.stream(scan.nextLine()
                    .split("\\|"))
                    .map(String::trim)
                    .toArray(String[]::new);

            String town = tokens[0];
            double income = Double.parseDouble(tokens[1]);

            if (!townsIncomes.containsKey(town)){
                townsIncomes.put(town, income);
            } else {
                townsIncomes.put(town, townsIncomes.get(town) + income);
            }
        }

        for (String town : townsIncomes.keySet()) {
            System.out.println(town + " -> " + townsIncomes.get(town));
        }
    }
}
