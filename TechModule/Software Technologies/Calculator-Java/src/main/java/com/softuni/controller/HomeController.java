package com.softuni.controller;

import com.softuni.entity.Calculator;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestParam;

@Controller
public class HomeController {
    @GetMapping("/")
    public String index(Model model) {
        model.addAttribute("operator", "+");
        model.addAttribute("view", "views/calculatorForm");
        return "base-layout";
    }

    @PostMapping("/")
    public String calculate(@RequestParam String leftOperand,
                            @RequestParam String rightOperand,
                            @RequestParam String operator,
                            Model model) {

        Calculator calculator = new Calculator(leftOperand, rightOperand, operator);

        model.addAttribute("leftOperand", leftOperand);
        model.addAttribute("rightOperand", rightOperand);
        model.addAttribute("operator", operator);

        String errorMsg = calculator.errorMsg();
        if (errorMsg.equals("no error")) {
            double result = calculator.calculateResult();
            model.addAttribute("result", result);
        } else {
            model.addAttribute("errorMsg", errorMsg);
            String result = "-";
            model.addAttribute("result", result);
        }

        model.addAttribute("view", "views/calculatorForm");

        return "base-layout";
    }
}

