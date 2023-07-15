namespace Gamification.App.Models
{
    public class SocketMessage
    {
        public string ReceiveId { get; set; }
        public string Message { get; set; }

        public SocketMessage(string message, string receiveId)
        {
            Message = message;
            ReceiveId = receiveId;
        }
    }
}
