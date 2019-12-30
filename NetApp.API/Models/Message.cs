using System;

namespace NetApp.API.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int MessageSenderId { get; set; }
        public User MessageSender { get; set; }
        public int MessageRecipientId { get; set; }
        public User MessageRecipient { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; }
        public bool SenderDeleted { get; set; }
        public bool RecipientDeleted { get; set; }
    }
}