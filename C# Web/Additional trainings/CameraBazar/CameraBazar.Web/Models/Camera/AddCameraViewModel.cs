namespace CameraBazar.Web.Models.Camera
{
    using System.Collections.Generic;
    using Data.Models.Enums;
    using System.ComponentModel.DataAnnotations;
    using Infrastructure;

    public class AddCameraViewModel
    {
        public CameraMake Make { get; set; }

        [Required]
        [StringLength(100, 
            ErrorMessage = Constants.ValidationErrors.StringLength)]
        [RegularExpression("[A-Z0-9-]+",
            ErrorMessage = Constants.ValidationErrors.UppercaseLettersDigitsAndDashOnly)]
        public string Model { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [Range(0, 100)]
        public int Quantity { get; set; }

        [Range(1, 30)]
        [Display(Name = "Minimum shutter speed")]
        public int MinShutterSpeed { get; set; }

        [Range(2000, 8000)]
        [Display(Name = "Maximum shutter speed")]
        public int MaxShutterSpeed { get; set; }

        [Display(Name = "Minimum ISO")]
        public MinIso MinIso { get; set; }

        [Range(200, 409600)]
        [RegularExpression("[0-9]+00",
        ErrorMessage = Constants.ValidationErrors.DividedBy100)]
        [Display(Name = "Maximum ISO")]
        public int MaxIso { get; set; }

        [Display(Name = "Full frame")]
        public bool IsFullFrame { get; set; }

        [Required]
        [StringLength(15,
            ErrorMessage = Constants.ValidationErrors.StringLength)]
        [Display(Name = "Video resolution")]
        public string VideoResolution { get; set; }

        [Required]
        [Display(Name = "Light metering")]
        public IEnumerable<LightMetering> LightMetering { get; set; }

        [Required]
        [StringLength(6000)]
        public string Description { get; set; }

        [Required]
        [StringLength(2000,
            ErrorMessage = Constants.ValidationErrors.StringLength,
            MinimumLength = 10)]
        [RegularExpression("http://.+|https://.+",
            ErrorMessage = Constants.ValidationErrors.Url)]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }
    }
}
