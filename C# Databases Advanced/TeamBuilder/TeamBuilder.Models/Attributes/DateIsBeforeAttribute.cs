namespace TeamBuilder.Models.Attributes
{
	using System;
	using System.ComponentModel.DataAnnotations;

    public class DateIsBeforeAttribute : ValidationAttribute
    {
        public DateTime CompareDateTime { get; set; }

        public override bool IsValid(object value)
        {
            return (DateTime)value < this.CompareDateTime;
        }
    }
}
