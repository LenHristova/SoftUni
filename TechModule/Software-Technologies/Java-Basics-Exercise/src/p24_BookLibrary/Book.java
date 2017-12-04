package p24_BookLibrary;

import java.time.LocalDate;

public class Book {
    private String title;
    private String author;
    private String publisher;
    private LocalDate date;
    private String isbn;
    private double price;

    public Book(String title, String author, String publisher,
                LocalDate date, String isbn, double price) {
        this.title = title;
        this.author = author;
        this.publisher = publisher;
        this.date = date;
        this.isbn = isbn;
        this.price = price;
    }

    public String getAuthor() {
        return author;
    }

    public double getPrice() {
        return price;
    }
}
