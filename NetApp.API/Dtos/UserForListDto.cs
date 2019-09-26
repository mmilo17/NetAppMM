using System;

namespace NetApp.API.Dtos
{
    public class UserFroListDto
    {
        public int Id {get; set; }

        public string Username {get; set; }
        
        public string Name { get; set; }

        public string Surnname { get; set; }
        
        public string Gender { get; set; }

        public int Age { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public string Bio { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string PhotoUrl { get; set; }
    }
}