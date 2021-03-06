using System;
using System.Collections.Generic;

namespace NetApp.API.Models
{
    public class User
    {
        public int Id {get; set; }

        public string Username {get; set; }

        public byte[] PasswordHash {get; set; }

        public byte[] PasswordSalt {get; set; }
        
        public string Name { get; set; }

        public string Surnname { get; set; }
        
        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public string Bio { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
        
        public virtual ICollection<Request> Senders { get; set; } //Likers
        
        public virtual ICollection<Request> Recivers { get; set; } //Likees

        public virtual ICollection<Message> MessagesSent { get; set; }
        
        public virtual ICollection<Message> MessagesReceived { get; set; }

    }
}