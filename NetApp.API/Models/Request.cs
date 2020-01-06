namespace NetApp.API.Models
{
    public class Request
    {
        public int SenderId { get; set; }//LikerId
        public int ReceiverId { get; set; }//LikeeId
        public virtual User Sender { get; set; }//Liker
        public virtual User Receiver { get; set; }//Likee

    }
}