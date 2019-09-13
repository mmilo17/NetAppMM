using System.ComponentModel.DataAnnotations;

namespace NetApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username {get; set; }
        
        [Required]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "You must specify password between 4- 16 characters")]
        public string Password {get; set; }
    }
}