namespace CarDealer.Web.Models.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CarCreateModel
    {
        [Required]
        [MaxLength(50, 
            ErrorMessage = Constants.ValidationError.MaxLength)]
        public string Make { get; set; }

        [Required]
        [MaxLength(50,
            ErrorMessage = Constants.ValidationError.MaxLength)]
        public string Model { get; set; }

        [Display(Name = "Traveled distance")]
        [Range(0, long.MaxValue)]
        public long TraveledDistance { get; set; }

        [Display(Name = "Parts")]
        public IEnumerable<int> SelectedPartsIds { get; set; }

        public IEnumerable<SelectListItem> Parts { get; set; }
    }
}
