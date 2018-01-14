using System.ComponentModel.DataAnnotations;

namespace Dating.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Password length from 6 to 32 characters.")]
        public string Password { get; set; }
    }
}