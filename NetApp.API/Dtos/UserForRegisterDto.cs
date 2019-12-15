using System;
using System.ComponentModel.DataAnnotations;

namespace NetApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username {get; set; }
        
        [Required]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "You must specify password between 4 - 16 characters")]
        public string Password {get; set; }
        [Required]
        public string Gender {get; set; }
        [Required]
        public string Name {get; set; }
        [Required]
        public string Surname {get; set; }
        [Required]
        public DateTime DateOfBirth {get; set; }
        [Required]
        public string City {get; set; }
        [Required]
        public string Country {get; set; }
        public DateTime Created {get; set; }
        public DateTime LastActive {get; set; }

        public UserForRegisterDto()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }
    }
}