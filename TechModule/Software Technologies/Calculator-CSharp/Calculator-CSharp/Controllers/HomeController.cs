using System;
using System.Web.Mvc;
using System.Web.WebPages;
using Calculator_CSharp.Models;

namespace Calculator_CSharp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(Calculator calculator)
        {
            return View(calculator);
        }

        [HttpPost]
        public ActionResult Calculate(Calculator calculator)
        {
            calculator.ErrorMsg = ErrorMsg(calculator);

            if (calculator.ErrorMsg == null)
            {
                calculator.Result = CalculateResult(calculator);
            }
            else
            {
                calculator.Result = "-";
            }      
            
            return RedirectToAction("Index", calculator);
        }

        private string CalculateResult(Calculator calculator)
        {
            var result = 0M;
            var leftOperand = decimal.Parse(calculator.LeftOperand);
            var rightOperand = decimal.Parse(calculator.RightOperand);

            switch (calculator.Operator)
            {
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
                case "%":
                    result = leftOperand % rightOperand;
                    break;               
            }

            return result.ToString("G");
        }

        public string ErrorMsg(Calculator calculator)
        { 
            if (calculator.LeftOperand == null || calculator.RightOperand == null)
            {
                return "The fields can not be empty!";
            }

            if (!decimal.TryParse(calculator.LeftOperand, out decimal dec) || !decimal.TryParse(calculator.RightOperand, out dec))
            {
                return "Invalid input! Please Enter a number!";
            }

            if ((calculator.Operator == "/" || calculator.Operator == "%") && calculator.RightOperand == "0")
            {
                return "Can not be deleted by zero!";
            }

            return null;
        }
    }
}