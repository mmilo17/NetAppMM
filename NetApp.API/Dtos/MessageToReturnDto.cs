using System;

namespace NetApp.API.Dtos
{
    public class MessageToReturnDto
    {
        public int Id { get; set; }
        public int MessageSenderId { get; set; }
        public string MessageSenderUsername { get; set; }
        public string MessageSenderPhotoUrl { get; set; }
        public int MessageRecipientId { get; set; }
        public string MessageRecipientUsername{ get; set; }
        public string MessageRecipientPhotoUrl { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; }
    }
}