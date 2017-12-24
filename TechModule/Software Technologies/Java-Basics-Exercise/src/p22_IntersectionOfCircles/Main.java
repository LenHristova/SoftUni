package p22_IntersectionOfCircles;

import java.util.Arrays;
import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);

        int[] circle1Params = Arrays
                .stream(scan.nextLine().split(" "))
                .mapToInt(Integer::parseInt)
                .toArray();
        int[] circle2Params = Arrays
                .stream(scan.nextLine().split(" "))
                .mapToInt(Integer::parseInt)
                .toArray();
        Point center1 = new Point(circle1Params[0], circle1Params[1]);
        Point center2 = new Point(circle2Params[0], circle2Params[1]);
        int radius1 = circle1Params[2];
        int radius2 = circle1Params[2];
        Circle circle1 = new Circle(center1, radius1);
        Circle circle2 = new Circle(center2, radius2);

        System.out.println(intersect(circle1, circle2) ? "Yes" : "No");
    }

    private static boolean intersect(Circle circle1, Circle circle2) {
        double distanceBtwCenters = Math.sqrt(Math.pow((circle1.getCenter().getX() - circle2.getCenter().getX()), 2) +
                                            (Math.pow((circle1.getCenter().getY() - circle2.getCenter().getY()), 2)));

        return distanceBtwCenters <= circle1.getRadius() + circle2.getRadius();
    }
}
