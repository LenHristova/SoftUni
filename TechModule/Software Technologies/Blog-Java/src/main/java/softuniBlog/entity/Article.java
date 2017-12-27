package softuniBlog.entity;

import org.hibernate.validator.constraints.Length;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.core.userdetails.UserDetails;

import javax.persistence.*;

@Entity
@Table(name = "articles")
public class Article {

    private Integer id;

    private String title;

    private String content;

    private User author;

    public Article(String title, String content, User author) {
        this.title = title;
        this.content = content;
        this.author = author;
    }

    public Article() {
    }

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    @Column(nullable = false)
    @Length(min = 1)
    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    @Column(columnDefinition = "text", nullable = false)
    @Length(min = 1)
    public String getContent() {
        return content;
    }

    public void setContent(String content) {
        this.content = content;
    }

    @ManyToOne()
    @JoinColumn(nullable = false, name = "authorId")
    public User getAuthor() {
        return author;
    }

    public void setAuthor(User author) {
        this.author = author;
    }

    @Transient
    public String getSummary() {

        if (this.getContent().length() < 350){
            return this.getContent();
        }

        return this.getContent().substring(0, 350) + "...";
    }

    @Transient
    public boolean isUserAuthor() {

        UserDetails loggedUser = (UserDetails) SecurityContextHolder.getContext()
                .getAuthentication().getPrincipal();

        return loggedUser.getUsername().equals(this.getAuthor().getEmail());

    }
}
