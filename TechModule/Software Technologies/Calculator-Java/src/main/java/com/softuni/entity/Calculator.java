package com.softuni.entity;

public class Calculator {
    private String leftOperand;
    private String rightOperand;
    private String operator;

    public String getLeftOperand() {
        return leftOperand;
    }

    public void setLeftOperand(String leftOperand) {
        this.leftOperand = leftOperand;
    }

    public String getRightOperand() {
        return rightOperand;
    }

    public void setRightOperand(String rightOperand) {
        this.rightOperand = rightOperand;
    }

    public String getOperator() {
        return operator;
    }

    public void setOperator(String operator) {
        this.operator = operator;
    }

    public Calculator(String leftOperand, String rightOperand, String operator) {
        this.leftOperand = leftOperand;
        this.rightOperand = rightOperand;
        this.operator = operator;
    }

    public String errorMsg() {
        String errorMsg = "no error";

        if (this.leftOperand.equals("") || this.rightOperand.equals("")) {
            return "Not all fields are filled in!";
        }

        double num1;
        double num2;
        boolean isLeftOperandNum = true;
        boolean isRightOperandNum = true;

        try {
            num1 = Double.parseDouble(leftOperand);
        } catch (NumberFormatException ex) {
            isLeftOperandNum = false;
        }

        try {
            num2 = Double.parseDouble(rightOperand);
        } catch (NumberFormatException ex) {
            isRightOperandNum = false;
        }

        if (!isLeftOperandNum || !isRightOperandNum) {
            if (!isLeftOperandNum) {
                this.leftOperand = "Invalid input!";
            }
            if (!isRightOperandNum) {
                this.rightOperand = "Invalid input!";
            }

            return "The operands must be numbers!";
        }

        if (this.operator.equals("/") && this.getRightOperand().equals("0")) {
            return "Can not be divided by zero!";
        }

        return errorMsg;
    }

    public double calculateResult() {
        double result;
        double leftOperand = Double.parseDouble(this.leftOperand);
        double rightOperand = Double.parseDouble(this.rightOperand);

        switch (operator) {
            case "+":
                result = leftOperand + rightOperand;
                break;
            case "-":
                result = leftOperand - rightOperand;
                break;
            case "*":
                result = leftOperand * rightOperand;
                break;
            case "/":
                result = leftOperand / rightOperand;
                break;
            default:
                result = 0;
                break;
        }

        return result;
    }
}
