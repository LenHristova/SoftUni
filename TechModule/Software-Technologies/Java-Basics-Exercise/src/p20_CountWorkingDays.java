import java.time.DayOfWeek;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.Locale;
import java.util.Scanner;

public class p20_CountWorkingDays {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);

        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("dd-MM-yyyy", Locale.ENGLISH);
        LocalDate startDate = LocalDate.parse(scan.nextLine(), formatter);
        LocalDate endDate = LocalDate.parse(scan.nextLine(), formatter);

        int workDaysCounter = 0;
        while (true) {
            if (startDate.isAfter(endDate)) {
                break;
            }

            if (!isHoliday(startDate)) {
                workDaysCounter++;
            }

            startDate = startDate.plusDays(1);
        }

        System.out.println(workDaysCounter);
    }

    private static boolean isHoliday(LocalDate day) {
        if (day.getDayOfWeek().equals(DayOfWeek.SATURDAY)  ||
                day.getDayOfWeek().equals(DayOfWeek.SUNDAY)) {
            return true;
        }

        int year = day.getYear();
        return day.equals(LocalDate.of(year, 1, 1)) ||
                day.equals(LocalDate.of(year, 3, 3)) ||
                day.equals(LocalDate.of(year, 5, 1)) ||
                day.equals(LocalDate.of(year, 5, 6)) ||
                day.equals(LocalDate.of(year, 5, 24)) ||
                day.equals(LocalDate.of(year, 9, 6)) ||
                day.equals(LocalDate.of(year, 9, 22)) ||
                day.equals(LocalDate.of(year, 11, 1)) ||
                day.equals(LocalDate.of(year, 12, 24)) ||
                day.equals(LocalDate.of(year, 12, 25)) ||
                day.equals(LocalDate.of(year, 12, 26));

    }
}