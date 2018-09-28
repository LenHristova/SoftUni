namespace CameraBazar.Web.Models.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum CameraAvailability
    {
        [Display(Name = "In Stock")]
        InStock,
        [Display(Name = "Out Of Stock")]
        OutOfStock
    }
}
