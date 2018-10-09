namespace IRunes.Web.ViewModels.Albums
{
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class CreateAlbumViewModel
    {
        [Required]
        [StringLength(200, MinimumLength = 3,
            ErrorMessage = Constants.ValidationErrorMessages.StringLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 10,
            ErrorMessage = Constants.ValidationErrorMessages.StringLength)]
        public string CoverImageUrl { get; set; }
    }
}
