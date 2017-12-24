package p25_BookLibrary;

import java.util.ArrayList;

public class Library {
    private String name;
    private ArrayList<Book> books = new ArrayList<>();

    public void addBook(Book book) {
        this.books.add(book);
    }

    public ArrayList<Book> getBooks() {

        return books;
    }
}
