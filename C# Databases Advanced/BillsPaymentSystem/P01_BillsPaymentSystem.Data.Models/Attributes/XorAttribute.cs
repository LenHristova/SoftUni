using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace P01_BillsPaymentSystem.Data.Models.Attributes
{
	using System;

		[AttributeUsage(AttributeTargets.Property)]
    public class XorAttribute : ValidationAttribute
		{
		    private readonly string relatedProperty;
		    private readonly IEnumerable<string> xorTargetAttributes;

		    public XorAttribute(string relatedProperty, string[] xorTargetAttributes)
		    {
		        this.relatedProperty = relatedProperty;
		        this.xorTargetAttributes = xorTargetAttributes;
		    }

		    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var targetAttributes = validationContext.ObjectType
                .GetProperties()
                .Where(p => this.xorTargetAttributes.Contains(p.Name))
                .Select(p => p.GetValue(validationContext.ObjectInstance))
                .ToList();
            var relatedPropertyValue = validationContext.ObjectType
                .GetProperty(this.relatedProperty)
                .GetValue(validationContext.ObjectInstance);


		    var errMsg = "One of the properties must be null.";

            var firstValidCondition = (value == null && relatedPropertyValue == null) && targetAttributes.Any(v => v != null);
            var secondValidCondition = (value != null || relatedPropertyValue != null) && targetAttributes.All(v => v == null);

            if (firstValidCondition || secondValidCondition)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(errMsg);
        }
    }
}
