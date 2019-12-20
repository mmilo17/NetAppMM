namespace NetApp.API.Models
{
    public class Request
    {
        public int SenderId { get; set; }//LikerId
        public int ReceiverId { get; set; }//LikeeId
        public User Sender { get; set; }//Liker
        public User Receiver { get; set; }//Likee

    }
}