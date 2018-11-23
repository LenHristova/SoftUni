namespace Eventures.Common.Models.Events
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Attributes.ValidationAttributes;
    using Constants;

    public class CreateEventInputModel : IValidatableObject
    {
        [Required]
        [StringLength(200,
            ErrorMessage = ValidationConstants.StringLength,
            MinimumLength = 10)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(200,
            ErrorMessage = ValidationConstants.StringLength,
            MinimumLength = 1)]
        [NotEmptyOrWhiteSpace]
        [Display(Name = "Place")]
        public string Place { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Start { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime End { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Total Tickets")]
        public int TotalTickets { get; set; }

        [DataType(DataType.Currency)]
        [Range(typeof(decimal),"0", "79228162514264337593543950335")]
        [Display(Name = "Price Per Ticket")]
        public decimal PricePerTicket { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Start <= DateTime.Now)
            {
                yield return new ValidationResult(
                    ValidationConstants.StartDateTime, 
                    new List<string>{ "Start" });
            }

            if (this.End <= this.Start)
            {
                yield return new ValidationResult(
                    ValidationConstants.EndDateTime, 
                    new List<string>{ "End" });
            }
        }
    }
}
