package softuniBlog.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import softuniBlog.bindingModel.ArticleBindingModel;
import softuniBlog.entity.Article;
import softuniBlog.entity.User;
import softuniBlog.repository.ArticleRepository;
import softuniBlog.repository.UserRepository;

@Controller
public class ArticleController {
    @Autowired
    private ArticleRepository articleRepository;

    @Autowired
    private UserRepository userRepository;

    @GetMapping("/article/create")
    @PreAuthorize("isAuthenticated()")
    public String createForm(Model model) {
        model.addAttribute("view", "article/create");
        return "base-layout";
    }

    @PostMapping("/article/create")
    @PreAuthorize("isAuthenticated()")
    public String createArticle(Model model, ArticleBindingModel articleBindingModel) {

        UserDetails loggedUser = (UserDetails) SecurityContextHolder.getContext()
                .getAuthentication().getPrincipal();

        User currentUser = this.userRepository.findByEmail(loggedUser.getUsername());

        Article article = new Article(
                articleBindingModel.getTitle(),
                articleBindingModel.getContent(),
                currentUser);

        String errorMsg = getErrorMsg(article);
        if (errorMsg.isEmpty()) {
            try {
                this.articleRepository.saveAndFlush(article);
                return "redirect:/article/" + article.getId();
            } catch (Exception ex) {
                errorMsg += "There was an error while creating article! Please try again!";
            }
        }

        model.addAttribute("article", article);
        model.addAttribute("view", "article/create");
        model.addAttribute("errorMsg", errorMsg);
        return "base-layout";
    }

    @GetMapping("/article/{id}")
    public String details(Model model, @PathVariable Integer id) {

        if (!this.articleRepository.exists(id)) {
            return "redirect:/";
        }

        Article article = this.articleRepository.findOne(id);

        model.addAttribute("userIsAuthor", article.isUserAuthor());
        model.addAttribute("article", article);
        model.addAttribute("view", "article/details");
        return "base-layout";
    }

    @GetMapping("/article/edit/{id}")
    @PreAuthorize("isAuthenticated()")
    public String edit(Model model, @PathVariable Integer id) {

        Article article = this.articleRepository.findOne(id);

        if (!this.articleRepository.exists(id) || !article.isUserAuthor()) {
            return "redirect:/";
        }

        model.addAttribute("article", article);
        model.addAttribute("view", "article/edit");
        return "base-layout";
    }

    @PostMapping("/article/edit/{id}")
    @PreAuthorize("isAuthenticated()")
    public String editArticle(Model model, @PathVariable Integer id, ArticleBindingModel articleBindingModel) {

        if (!this.articleRepository.exists(id)) {
            return "redirect:/";
        }

        Article article = this.articleRepository.findOne(id);

        String errorMsg = getErrorMsg(article);

        if (errorMsg.isEmpty()) {
            try {
                article.setTitle(articleBindingModel.getTitle());
                article.setContent(articleBindingModel.getContent());
                this.articleRepository.saveAndFlush(article);
                return "redirect:/article/" + article.getId();
            } catch (Exception ex) {
                errorMsg += "There was an error while updating article! Please try again!";
            }
        }

        model.addAttribute("article", article);
        model.addAttribute("view", "article/edit");
        model.addAttribute("errorMsg", errorMsg);
        return "base-layout";
    }

    @GetMapping("/article/delete/{id}")
    @PreAuthorize("isAuthenticated()")
    public String delete(Model model, @PathVariable Integer id) {

        Article article = this.articleRepository.findOne(id);

        if (!this.articleRepository.exists(id) || !article.isUserAuthor()) {
            return "redirect:/";
        }

        model.addAttribute("article", article);
        model.addAttribute("view", "article/delete");
        return "base-layout";
    }

    @PostMapping("/article/delete/{id}")
    @PreAuthorize("isAuthenticated()")
    public String deleteArticle(Model model, @PathVariable Integer id) {

        if (!this.articleRepository.exists(id)) {
            return "redirect:/";
        }

        Article article = this.articleRepository.findOne(id);
        String errorMsg = "";
        try {
            this.articleRepository.delete(article);
            return "redirect:/";
        }catch (Exception ex) {
            errorMsg = "There was an error while creating article! Please try again!";
        }

        model.addAttribute("article", article);
        model.addAttribute("view", "article/delete");
        model.addAttribute("errorMsg", errorMsg);
        return "base-layout";
    }

    private String getErrorMsg(Article article) {
        String errorMsg = "";
        if (article.getTitle().equals("") || article.getContent().equals("")) {

            if (article.getTitle().length() > 255) {
                errorMsg = "The title is too long! ";
            }
            if (article.getTitle().equals("")) {
                errorMsg += "Please add title! ";
            }

            if (article.getContent().equals("")) {
                errorMsg += "Please add content!";
            }
        }
        return errorMsg;
    }
}
