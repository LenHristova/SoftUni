package p24_BookLibrary;

import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.*;

public class Main {
    public static void main(String[] args) {
        Scanner scan = new Scanner(System.in);

        Library library = new Library();

        int bookCount = Integer.parseInt(scan.nextLine());

        for (int i = 0; i < bookCount; i++) {
            String[] bookInfo = scan.nextLine().split(" ");

            String title = bookInfo[0];
            String author = bookInfo[1];
            String publisher = bookInfo[2];
            DateTimeFormatter formatter = DateTimeFormatter.ofPattern("dd.MM.yyyy", Locale.ENGLISH);
            LocalDate date = LocalDate.parse(bookInfo[3], formatter);
            String isbn = bookInfo[4];
            double price = Double.parseDouble(bookInfo[5]);

            Book currBook = new Book(title, author, publisher, date, isbn, price);
            library.addBook(currBook);
        }

        HashMap<String, Double> authorPrice = new HashMap<>();
        authorPrice = update(authorPrice, library);

        // Sorting by Price and then by Author
        Map<String, Double> sortedAuhorsByPrice = new LinkedHashMap<>();
        authorPrice.entrySet().stream()
                .sorted(Map.Entry.<String, Double>comparingByKey())
                .sorted(Map.Entry.<String, Double>comparingByValue().reversed())
                .forEachOrdered(x -> sortedAuhorsByPrice.put(x.getKey(), x.getValue()));

        // Printing the result
        sortedAuhorsByPrice.forEach((key, val) -> System.out.printf("%s -> %1.2f\n", key, val));
    }

    private static HashMap<String, Double> update(HashMap<String, Double> authorPrice, Library library) {
        for (Book book : library.getBooks()) {
            if (!authorPrice.containsKey(book.getAuthor())){
                authorPrice.put(book.getAuthor(), book.getPrice());
            }else {
                authorPrice.put(book.getAuthor(), authorPrice.get(book.getAuthor()) + book.getPrice());
            }
        }

        return authorPrice;
    }
}

