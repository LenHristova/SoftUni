namespace IRunes.Web.ViewModels.Tracks
{
    using Common;
    using System.ComponentModel.DataAnnotations;

    public class CreateTrackViewModel
    {
        [Required]
        [StringLength(200, MinimumLength = 3,
            ErrorMessage = Constants.ValidationErrorMessages.StringLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 10,
            ErrorMessage = Constants.ValidationErrorMessages.StringLength)]
        public string VideoUrl { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }
    }
}
