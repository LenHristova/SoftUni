package p23_AverageGrades;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Comparator;
import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);

        int count = Integer.parseInt(scan.nextLine());

        ArrayList<Student> studentsGrades = new ArrayList<>();
        for (int i = 0; i < count; i++) {
            String[] studentInfo = scan.nextLine().split(" ");

            String name = studentInfo[0];
            double[] grades = new double[studentInfo.length - 1];
            grades = updateGrades(grades, studentInfo);

            Student currStudent = new Student(name, grades);
            studentsGrades.add(currStudent);
        }

        studentsGrades.removeIf(st -> st.getAverageGrade() < 5);

        studentsGrades.sort(Comparator.comparing(Student::getName)
                .thenComparing(Comparator.comparing(Student::getAverageGrade).reversed()));

        for (Student studentGrade : studentsGrades) {
            System.out.printf("%s -> %.2f%n",
                    studentGrade.getName(),
                    studentGrade.getAverageGrade());
        }
    }

    private static double[] updateGrades(double[] grades, String[] studentInfo) {

        for (int i = 1; i < studentInfo.length; i++) {
            grades[i - 1] = Double.parseDouble(studentInfo[i]);
        }

        return grades;
    }
}
