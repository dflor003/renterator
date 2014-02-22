namespace Renterator.Services.Infrastructure
{
    public class LogMessage
    {
        public LogMessage(MessageType type, string message)
        {
            this.Type = type;
            this.Message = message;
        }

        public MessageType Type { get; set;}

        public string Message { get; set; }
    }
}