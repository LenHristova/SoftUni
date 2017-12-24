using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace Calculator_CSharp.Models
{
    public class Calculator
    {
        public string LeftOperand { get; set; } 

        public string RightOperand { get; set; } 

        public string Operator { get; set; }

        public string Result { get; set; }

        public string ErrorMsg { get; set; }
    }
}