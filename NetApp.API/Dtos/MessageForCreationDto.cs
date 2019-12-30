using System;

namespace NetApp.API.Dtos
{
    public class MessageForCreationDto
    {
        public int MessageSenderId { get; set; }
        public int MessageRecipientId { get; set; }
        public DateTime MessageSent { get; set; }
        public string Content { get; set; }
        public MessageForCreationDto()
        {
            MessageSent = DateTime.Now;
        }
    }
}