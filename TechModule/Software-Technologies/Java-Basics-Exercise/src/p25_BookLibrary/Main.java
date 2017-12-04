package p25_BookLibrary;

import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.*;
import java.util.stream.Stream;

public class Main {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);

        Library library = new Library();

        int bookCount = Integer.parseInt(scan.nextLine());
        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("dd.MM.yyyy", Locale.ENGLISH);
        for (int i = 0; i < bookCount; i++) {
            String[] bookInfo = scan.nextLine().split(" ");

            String title = bookInfo[0];
            String author = bookInfo[1];
            String publisher = bookInfo[2];
            LocalDate date = LocalDate.parse(bookInfo[3], formatter);
            String isbn = bookInfo[4];
            double price = Double.parseDouble(bookInfo[5]);

            Book currBook = new Book(title, author, publisher, date, isbn, price);
            library.addBook(currBook);
        }

        LocalDate searchedDate = LocalDate.parse(scan.nextLine(), formatter);

        Book[] filteredBooks = library.getBooks().stream().filter(b -> b.getDate().isAfter(searchedDate)).toArray(Book[]::new);

        HashMap<String, LocalDate> titleDate = new HashMap<>();
        titleDate = update(titleDate, filteredBooks);

        // Sorting by Date and then by Title
        Map<String, LocalDate> sortedTitlesByDate = new LinkedHashMap<>();
        titleDate.entrySet().stream()
                .sorted(Map.Entry.<String, LocalDate>comparingByKey())
                .sorted(Map.Entry.<String, LocalDate>comparingByValue())
                .forEachOrdered(x -> sortedTitlesByDate.put(x.getKey(), x.getValue()));

        // Printing the result
        sortedTitlesByDate.forEach((key, val) -> System.out.println(key + " -> " + val.format(formatter)));
    }

    private static HashMap<String, LocalDate> update(HashMap<String, LocalDate> titleDate, Book[] books) {
        for (Book book : books) {
                titleDate.put(book.getTitle(), book.getDate());
        }

        return titleDate;
    }
}

