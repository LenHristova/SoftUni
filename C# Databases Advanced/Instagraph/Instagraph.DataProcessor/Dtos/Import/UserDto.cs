namespace Instagraph.DataProcessor.Dtos.Import
{
    using System.ComponentModel.DataAnnotations;

    public class UserDto
    {
        [Required]
        [StringLength(30, MinimumLength = 1)]
        public string Username { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Password { get; set; }

        [Required]
        public string ProfilePicture { get; set; }
    }
}
