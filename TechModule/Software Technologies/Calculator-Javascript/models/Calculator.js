function Calculator(leftOperand, operator, rightOperand) {
    this.leftOperand = leftOperand;
    this.operator = operator;
    this.rightOperand = rightOperand;

    this.calculateResult = function () {
        let result = "Invalid input!";

        if (this.leftOperand == "" || this.rightOperand == "") {
            return "Not all fields are filled in!"
        } else {
            switch (this.operator) {
                case "+":
                    result = Number(this.leftOperand) + Number(this.rightOperand);
                    break;
                case "-":
                    result = this.leftOperand - this.rightOperand;
                    break;
                case "*":
                    result = this.leftOperand * this.rightOperand;
                    break;
                case "/":
                    if (this.rightOperand == "0") {
                        return "Can not be divided by zero!"
                    }
                    result = this.leftOperand / this.rightOperand;
                    break;
                case "%":
                    if (this.rightOperand == "0") {
                        return "Can not be divided by zero!"
                    }
                    result = this.leftOperand % this.rightOperand;
            }
        }

        return isNaN(result) ? "Invalid input!" : result;
    }
}

module.exports = Calculator;