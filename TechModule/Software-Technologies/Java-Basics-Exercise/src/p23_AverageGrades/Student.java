package p23_AverageGrades;

public class Student {
    private String name;
    private double[] grades;
    private double averageGrade;

    public Student(String name, double[] grades) {
        this.name = name;
        this.grades = grades;
    }

    public String getName() {
        return name;
    }

    public double getAverageGrade() {
        double sum = 0;
        for (double grade : grades) {
            sum += grade;
        }
        averageGrade = sum / grades.length;

        return averageGrade;
    }
}
