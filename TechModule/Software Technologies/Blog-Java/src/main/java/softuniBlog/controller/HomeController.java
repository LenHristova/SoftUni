package softuniBlog.controller;

import org.hibernate.mapping.Collection;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import softuniBlog.entity.Article;
import softuniBlog.repository.ArticleRepository;

import java.util.Collections;
import java.util.List;
import java.util.stream.Collectors;
import java.util.stream.Stream;

@Controller
public class HomeController {

    @Autowired
    private ArticleRepository articleRepository;

    @GetMapping("/")
    public String index(Model model) {

        List<Article> articles = this.articleRepository
                .findAll();
        Collections.reverse(articles);

        articles = articles.stream()
                .limit(10)
                .collect(Collectors.toList());

        model.addAttribute("view", "home/index");
        model.addAttribute("articles", articles);
        return "base-layout";
    }
}
